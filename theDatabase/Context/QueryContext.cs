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
        public string Matchcode { get; set; } = string.Empty;

        public IItemType ItemType { get; set; } = null;
        public IDTO RelatedItem { get; set; } = null;
        public IDTO Item { get; set; } = null;
        
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

            IQueryResult result = await sqlService.GetItems(query);
            var rawItems = result.Items ?? new List<IDTO>();
            var userGuid = secService.User.GUID;

            // 1. Schritt: Sicherheits- & Rechte-Filterung (IsPrivate)
            IEnumerable<IDTO> filteredItems = rawItems.Where(se =>
                !se["IsPrivate"].ToSecureBool() ||
                (se["IsPrivate"].ToSecureBool() && ((IMetadata)se).Metadata.CreatedBy == userGuid)
            );

            // 2. Schritt: Matchcode-Filterung (falls ein Suchbegriff vorhanden ist)
            string search = this.Matchcode.ToSecureString();
            if (!string.IsNullOrWhiteSpace(search))
            {
                // Suchbegriffe am Leerzeichen aufsplitten und leere Einträge entfernen
                string[] searchTerms = search.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                filteredItems = filteredItems.Where(se =>
                    se.Matchcode != null &&
                    searchTerms.Any(term => se.Matchcode.Contains(term, StringComparison.OrdinalIgnoreCase))
                );
            }

            // Erst am Ende wird die gefilterte Sequenz in die finale Liste umgewandelt
            Items = filteredItems.ToList();

            IsLoading = false;
        }
        public async Task Filter(string matchcode)
        {
            await Search();

            if (!string.IsNullOrEmpty(matchcode))
            { 
                Items = Items.Where(se => se.Matchcode.ToLowerInvariant().Contains(matchcode.ToLowerInvariant())).ToList();
            }
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

                if (result.Items[0] != null)
                {
                    Item = result.Items[0];
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
                return;

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

            if (this.RelatedItem != null)
            {
                IQueryResult result = await sqlService.Remove(this.RelatedItem, dto);
            }

        }

        // Dispose 
        public void Dispose()
        {
            try
            {
                ItemType = null;
                RelatedItem = null;
                Item = null;
                Items = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : DISPOSE : BoardComp : " + ex.Message);
            }
        }
    }
}
