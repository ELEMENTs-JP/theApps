using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Template : BaseItemType, IItemType
    {
        ISqlDatabaseService sqlService;
        IDTO Item { get; set; }
        public ItemType_Template(IDTO dto, ISqlDatabaseService sql)
        {
            sqlService = sql;
            Item = dto;

            this.Name = dto.Title;
            this.Group = dto["Group"].ToSecureString();
            this.InSubNavigation = dto["InSubNavigation"].ToSecureBool();
            this.Typ = dto["ItemTypeTyp"].ToSecureString().ToEnumOrDefault<ItemTypeTyp>(ItemTypeTyp.Item);
        }

        public override async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new List<IItemType>();

            // ItemTypes 
            if (sqlService == null)
                return ItemTypes;

            // Query 
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = sqlService.MasterGUID;
            query.ItemType = "ItemType";
            IQueryResult result = await sqlService.GetRelatedItems(Item, "ItemType", "");

            // Iteration 
            foreach (IDTO it in result.Items)
            {
                IItemType itemtype = new ItemType_Template(it, sqlService);
                ItemTypes.Add(itemtype);
            }

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            List<IField> Fields = new();

            // Fields 
            if (sqlService == null)
                return Fields;

            // Query 
            IQueryParameter query = new QueryParameter();
            query.Matchcode = string.Empty;
            query.MasterGUID = sqlService.MasterGUID;
            query.ItemType = "Field";
            // Fields 
            IQueryResult result = await sqlService.GetRelatedItems(Item, "Field");

            // Iteration 
            foreach (IDTO it in result.Items)
            {
                IField field = new Field();
                field.Title = it.Title;
                field.Column = it.Title;
                field.Typ = (FieldTyp)Enum.Parse(typeof(FieldTyp), it["Typ"].ToSecureString());

                Fields.Add(field);
            }

            return Fields;
        }

       
    }
}
