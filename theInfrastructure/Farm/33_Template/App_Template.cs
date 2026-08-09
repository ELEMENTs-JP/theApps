using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Template : BaseApp, IApp
    {
        ISqlDatabaseService sqlService;
        IDTO Item { get; set; }
        public App_Template(IDTO Item, ISqlDatabaseService sql)
        {
            // Service 
            sqlService = sql;
            this.Item = Item;
            // Metadata 
            this.Name = Item.Title;

            Init();
        }

        public override async Task Init()
        {
            await base.Init();

            // ItemTypes 
            if (sqlService == null)
                return;

            // Query 
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = sqlService.MasterGUID;
            query.ItemType = "ItemType";
            IQueryResult result = await sqlService.GetRelatedItems(Item, "ItemType");

            // Iteration 
            foreach (IDTO it in result.Items)
            {
                IItemType itemtype = new ItemType_Template(it, sqlService);
                this.ItemTypes.Add(itemtype);
            }
        }
    }

}
