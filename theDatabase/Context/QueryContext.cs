using System;
using System.Collections.Generic;
using System.Text;
using theInfrastructure;

namespace theDatabase
{
    public class QueryContext : IQueryContext, IDisposable
    {
        public bool IsLoading { get; set; } = false;

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
            query.Matchcode = string.Empty;
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
        public async Task RelatedItems(string itemType)
        {
            if (sqlService == null)
                return;

            if (ItemType == null)
                return;

            if (RelatedItem == null)
                return;

            IsLoading = true;

            Items.Clear();
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = SQLiteService.GeneralMasterGUID;
            query.ItemType = ItemType.Name;
            IQueryResult result = await sqlService.GetRelatedItems(RelatedItem, itemType);
            Items = result.Items;

            IsLoading = false;
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

            if (this.Item == null)
                return;

            IQueryResult result = await sqlService.Remove(this.Item, dto);

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
