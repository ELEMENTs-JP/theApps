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
            this.Group = Item["Group"].ToSecureString();
            this.IsActive = Item["IsActive"].ToSecureBool();
        }

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();
            // ItemTypes 
            if (sqlService == null)
                return ItemTypes;

            // Query 
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = sqlService.MasterGUID;
            query.ItemType = "ItemType";
            IQueryResult result = await sqlService.GetRelatedItems(Item, "ItemType", ast);

            // Iteration 
            foreach (IDTO it in result.Items)
            {
                IItemType itemtype = new ItemType_Template(it, sqlService);
                ItemTypes.Add(itemtype);
            }

            return ItemTypes;
        }

        public override async Task<List<IDTO>> GetPages()
        {
            // Query 
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = sqlService.MasterGUID;
            query.ItemType = "Page";

            // Fields 
            IQueryResult result = await sqlService.GetRelatedItems(Item, "Page");
            return result.Items;
        }
    }

}
