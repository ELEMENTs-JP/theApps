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
        public string Matchcode { get; set; } = string.Empty;

        ISqlDatabaseService sqlService = null;
        public IItemType ItemType { get; set; } = null;
        public IDTO RelatedItem { get; set; } = null;
        public IDTO Item { get; set; } = null;
        public List<IDTO> Items { get; set; } = new List<IDTO>();
        public QueryContext()
        {

        }
        public QueryContext(ISqlDatabaseService sql)
        {
            Init(sql);
        }
        public void Init(ISqlDatabaseService sql)
        {
            sqlService = sql;
        }
        public async Task<Guid> Create(string title)
        {
            if (sqlService == null)
                return Guid.Empty;

            IQueryParameter qp = QueryParameter.Default(title);
            Guid gid = qp.GUID;
            qp.ItemType = ItemType.Name;
            await sqlService.Create(qp);
            return gid;
        }
        public async Task Search()
        {
            if (sqlService == null)
                return;

            if (ItemType == null)
                return;

            IsLoading = true;

            Items.Clear();
            IQueryParameter query = new QueryParameter();
            query.Matchcode = this.Matchcode.ToSecureString();
            query.MasterGUID = SQLiteService.GeneralMasterGUID;
            query.ItemType = ItemType.Name;
            IQueryResult result = await sqlService.GetItems(query);
            Items = result.Items;

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
        public async Task<List<IDTO>> RelatedItems(string itemType)
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
            IQueryResult result = await sqlService.GetRelatedItems(RelatedItem, itemType);
            _items = result.Items;

            IsLoading = false;

            return _items;
        }
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
        public async Task Assign(IDTO dto)
        {
            if (sqlService == null)
                return;

            if (this.Item == null)
                return;

            IQueryResult result = await sqlService.Assign(this.Item, dto);

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
