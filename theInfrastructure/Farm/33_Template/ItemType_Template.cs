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

            // Fields 
            result = await sqlService.GetRelatedItems(Item, "Field");

            // Iteration 
            foreach (IDTO it in result.Items)
            {
                IField field = new Field();
                field.Title = it.Title;
                field.Column = it.Title;
                field.Typ = FieldTyp.Text;

                this.Fields.Add(field);
            }
        }
    }
}
