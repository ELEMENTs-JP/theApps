using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
 




        // Einmalig beim Start initialisiert – 0 Allokationen zur Laufzeit
        public static readonly HashSet<FieldTyp> AllowedFINDitsFieldTypes = new()
        {
            FieldTyp.Text, FieldTyp.CheckBox, FieldTyp.DropDown, FieldTyp.Priority, FieldTyp.Status, FieldTyp.Progress
        };

        public static bool IsAllowedFieldTyp(string rawType)
        {
            return Enum.TryParse<FieldTyp>(rawType, true, out var parsed)
                   && AllowedFINDitsFieldTypes.Contains(parsed);
        }

        public static async Task<List<IDTO>> Query(ISqlDatabaseService sql, IDTO Item, List<IDTO> Items)
        {
            IQueryParameter qp = QueryParameter.Default(".");
            qp.MasterGUID = sql.MasterGUID;
            qp.ItemType = Item["ItemType"].ToSecureString();
            qp.Matchcode = Item["Matchcode"].ToSecureString();

            IQueryResult result = await sql.GetItems(qp);
            Items = result.Items;

            // Filter 
            List<IDTO> multipleFilter = await Item.GetRelatedItems(sql, "SearchFilter");

            foreach (IDTO filter in multipleFilter)
            {
                string column = filter["Column"].ToSecureString();
                string typ = filter["Typ"].ToSecureString();
                string value = Item[column].ToSecureString();

                if (typ == "Text")
                {
                    Items = Items.Where(se => se[column].ToSecureString() == value).ToList();
                }
            }

            return Items;
        }
    }
}
