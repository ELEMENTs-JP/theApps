using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_Strategyzer : TemplateApp, ITemplateApp
    {
        public TApp_Strategyzer()
        {
            ID = "STRATEGY";
            Name = "STRATEGYzer";
            Title = "the STRATEGYzer";
            Group = "Strategy";
        }

  
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Geschäftsfeld", "Geschäftsfeld"));
            types.Add(new ItemTypeInfo("Portfolio", "Portfolio"));
            types.Add(new ItemTypeInfo("Strategy", "Strategy"));
            types.Add(new ItemTypeInfo("Initiative", "Initiative"));
            //types.Add(new ItemTypeInfo("Scorecard", "Business Scorecard"));
            //types.Add(new ItemTypeInfo("KPI", "Key Performance Indicator"));
            //types.Add(new ItemTypeInfo("TBI", "Time Based Indicator"));
            //types.Add(new ItemTypeInfo("SWOT", "SWOT Analysis"));
            //types.Add(new ItemTypeInfo("PESTEL", "PESTEL Analysis"));


            return types;
        }
        public override List<ItemTypeConnection> GetItemTypeConnections()
        {
            List<ItemTypeConnection> cons = new List<ItemTypeConnection>();

            // cons.Add(new ItemTypeConnection("BusinessModel", "ValueProposition")); // Parent -> Child 
 

            return cons;
        }
        public override List<FieldInfo> GetFields(string ItemType)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            //if (ItemType == "Template")
            //{
            //    fields.Add(new FieldInfo("Template", "string"));
              
            //}

            return fields;
        }
    }
}
