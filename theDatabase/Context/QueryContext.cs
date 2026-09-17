using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using theInfrastructure;

namespace theDatabase
{
    public class QueryContext : IQueryContext, IDisposable
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public bool IsLoading { get; set; } = false;
        ISqlDatabaseService sqlService = null;
        ISecurityService secService = null;

        // Query 
        public IFilterParameter Filter { get; set; } = new FilterParameter();

        public IItemType ItemType { get; set; } = null;
        public IDTO RelatedItem { get; set; } = null;
        public IDTO Item { get; set; } = null;
        public List<string> Groups { get; set; } = new List<string>();

        // Result 
        public List<IDTO> Items { get; set; } = new List<IDTO>();

        // ctr 
        public QueryContext()
        {

        }
        public QueryContext(ISqlDatabaseService sql, ISecurityService sec)
        {
            Init(sql, sec);
        }
        public QueryContext(ISqlDatabaseService sql, ISecurityService sec, IItemType ItemType)
        {
            Init(sql, sec);

            this.ItemType = ItemType;
        }
        public void Init(ISqlDatabaseService sql, ISecurityService sec)
        {
            sqlService = sql;
            secService = sec;
        }

        // Builder 
        public async Task<Guid> Create(string title)
        {
            if (sqlService == null)
                return Guid.Empty;

            IQueryParameter qp = QueryParameter.Default(title);
            Guid gid = qp.GUID;
            qp.MasterGUID = sqlService.MasterGUID;
            qp.ItemType = ItemType.Name;
            qp.UserGUID = secService.User.GUID;
            qp.UserName = secService.User.Title;

            await sqlService.Create(qp);

            return gid;
        }

        // Search 
        public async Task Search()
        {
            if (sqlService == null || ItemType == null)
                return;

            IsLoading = true;

            IQueryParameter query = new QueryParameter
            {
                Matchcode = string.Empty,
                MasterGUID = SQLiteService.GeneralMasterGUID,
                ItemType = ItemType.Name
            };

            // Personalisierte Abfrage 
            var userGuid = secService.User.GUID;
            query.IsPersonalizedQuery = ItemType.IsPersonalizedItemType();
            query.UserGUID = userGuid;

            // Query 
            IQueryResult result = await sqlService.GetItems(query);
            var rawItems = result.Items ?? new List<IDTO>();

            // IsPrivate: Entfernen von Items die als Private markiert wurden und nur mir gehören 
            IEnumerable<IDTO> filteredItems = rawItems.Where(se =>
                !se["IsPrivate"].ToSecureBool() ||
                (se["IsPrivate"].ToSecureBool() && ((IMetadata)se).Metadata.CreatedBy == userGuid)
            );

            // IsPersonal: Filterung auf Daten die nur von mir erstellt wurden oder mir zugewiesen wurden 

            // Matchcode 
            if (Filter != null)
            {
                string search = this.Filter.Matchcode.ToSecureString();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    // Suchbegriffe am Leerzeichen aufsplitten und leere Einträge entfernen
                    string[] searchTerms = search.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    filteredItems = filteredItems.Where(se =>
                        se.Matchcode != null &&
                        searchTerms.Any(term => se.Matchcode.Contains(term, StringComparison.OrdinalIgnoreCase))
                    );
                }
            }

            // Filter 
            if (this.Filter != null)
            {
                // OR Filterung 
                if (this.Filter.Parameters != null && this.Filter.Parameters.Any())
                {
                    filteredItems = filteredItems
                        .Where(se => this.Filter.Parameters.Any(p => se[p.Key] == p.Value))
                        .ToList();
                }
            }

            // Sortierung 
            if (this.Filter != null
                    && this.Filter.Direction != null
                        && this.Filter.SortColumn != string.Empty)
            {
                string sortColumn = this.Filter.SortColumn;

                if (this.Filter.Direction == System.ComponentModel.ListSortDirection.Ascending)
                {
                    filteredItems = filteredItems.OrderBy(se => se[sortColumn]);
                }
                else if (this.Filter.Direction == System.ComponentModel.ListSortDirection.Descending)
                {
                    filteredItems = filteredItems.OrderByDescending(se => se[sortColumn]);
                }
            }

            // Gruppierung 
            this.Groups.Clear();
            if (this.Filter != null && !string.IsNullOrEmpty(this.Filter.GroupColumn))
            {
                string groupColumn = this.Filter.GroupColumn;

                // Distinct Groups ermitteln    .Where(val => !string.IsNullOrWhiteSpace(val))
                this.Groups = filteredItems
                    .Select(se => se[groupColumn])

                    .Distinct()
                    .OrderBy(val => val)
                    .ToList();

                var grouped = filteredItems.GroupBy(se => se[groupColumn].ToSecureString());
                var resultList = new List<IDTO>();

                foreach (var group in grouped)
                {
                    string headerTitle = string.IsNullOrWhiteSpace(group.Key) ? "Ohne Zuordnung" : group.Key;

                    // 1. Gruppenheader-DTO einfügen
                    resultList.Add(new DTO { Title = headerTitle, ["GroupName"] = headerTitle });

                    // 2. Reguläre Datensätze der Gruppe anfügen
                    resultList.AddRange(group);
                }

                filteredItems = resultList;
            }

            // Sequenz in finale Liste 
            Items = filteredItems.ToList();

            IsLoading = false;
        }

        public async Task Load(string GUID)
        {
            try
            {
                if (sqlService == null)
                    return;

                if (string.IsNullOrEmpty(GUID))
                    return;

                IsLoading = true;

                Item = null;

                IQueryParameter query = new QueryParameter();
                query.Matchcode = string.Empty;
                query.MasterGUID = SQLiteService.GeneralMasterGUID;
                query.GUID = new Guid(GUID);
                query.ItemType = this.ItemType.Name;
                IQueryResult result = await sqlService.GetItem(query);

                if (result.Items.Count >= 1)
                {
                   Item = result.Items.FirstOrDefault();
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<List<IDTO>> RelatedItems(
            string itemType, AssociationTyp ast = AssociationTyp.Association)
        {
            List<IDTO> _items = new List<IDTO>();

            if (sqlService == null)
                return _items;

            if (ItemType == null)
                return _items;

            if (RelatedItem == null)
                return _items;

            IsLoading = true;

            _items.Clear();
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = SQLiteService.GeneralMasterGUID;
            query.ItemType = ItemType.Name;
            IQueryResult result = await sqlService.GetRelatedItems(RelatedItem, itemType, ast);
            _items = result.Items;

            IsLoading = false;

            return _items;
        }

        // Action 
        public async Task Delete(IDTO dto)
        {
            if (sqlService == null)
                return;

            await sqlService.Delete(dto);
        }
        public async Task Update(IDTO dto)
        {
            if (sqlService == null)
                return;

            if (dto == null)
                return;

            await sqlService.Update(dto);
        }
        public async Task ChangeItemType(IDTO dto, string newItemType)
        {
            if (sqlService == null)
                return;

            if (dto == null)
                return;

            await sqlService.ChangeItemType(dto, newItemType);
        }
        public async Task Assign(IDTO dto, AssociationTyp ast = AssociationTyp.Association)
        {
            if (sqlService == null)
                return;

            if (this.Item == null)
            {
                throw new Exception("Missing Item");
            }

            if (ast == AssociationTyp.Association ||
                ast == AssociationTyp.Default ||
                ast == AssociationTyp.Children ||
                ast == AssociationTyp.Parallels ||
                ast == AssociationTyp.Related)
            {
                // Item = Parent, dto = Child 
                await sqlService.Assign(this.Item, dto, ast);
            }
            else if (ast == AssociationTyp.Parents)
            {
                // DTO = Parent, Item = Child 
                await sqlService.Assign(dto, this.Item, ast);
            }
            else
            {
                // NULL 
                throw new Exception("Keine Association");
            }
        }
        public async Task Remove(IDTO dto)
        {
            if (sqlService == null)
                return;

            //if (this.Item != null)
            //{ 
            //    IQueryResult result = await sqlService.Remove(this.Item, dto);
            //}
            if (this.RelatedItem == null)
            {
                throw new Exception("Missing Related Item");
            }

            if (this.RelatedItem != null)
            {
                IQueryResult result = await sqlService.Remove(this.RelatedItem, dto);
            }

        }

        // Helper 
        public async Task Clear()
        {
            //Filter = null;
            //ItemType = null;

            //// Item 
            //Item = null;
            //RelatedItem = null;

            //// Items 
            //if (Items != null)
            //{
            //    Items.Clear();
            //}
        }
        public List<string> ValuesByColumn(string column)
        {
            return Items
         .Select(se => se[column])
         .Where(val => !string.IsNullOrWhiteSpace(val))
         .Distinct()
         .Order()
         .ToList();
        }

        // Dispose 
        public void Dispose()
        {
            try
            {
                //ItemType = null;
                //RelatedItem = null;
                //Item = null;
                //Items = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : DISPOSE : BoardComp : " + ex.Message);
            }
        }
    }
}
