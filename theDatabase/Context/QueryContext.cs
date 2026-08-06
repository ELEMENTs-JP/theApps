using System;
using System.Collections.Generic;
using System.Text;
using theInfrastructure;

namespace theDatabase
{
    public class QueryContext : IQueryContext
    {
        public bool IsLoading { get; set; } = false;

        ISqlDatabaseService sqlService = null;
        public IItemType ItemType { get; set; } = null;
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
        public async Task Create(string title)
        {
            if (sqlService == null)
                return;

            await sqlService.Create(QueryParameter.Default(title));
        }
        public async Task Search()
        {
            if (sqlService == null)
                return;

            IsLoading = true;

            Items.Clear();
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = SQLiteService.GeneralMasterGUID;
            IQueryResult result = await sqlService.GetItems(query);
            Items = result.Items;

            IsLoading = false;
        }
        public async Task Load(string GUID)
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
            IQueryResult result = await sqlService.GetItem(query);

            if (result.Items[0] != null)
            { 
                Item = result.Items[0];
            }

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
    }
}
