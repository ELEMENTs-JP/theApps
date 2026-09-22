using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_RISKler : TemplateApp, ITemplateApp
    {
        public TApp_RISKler()
        {
            ID = "RISK";
            Name = "RISKler";
            Title = "the RISKler";
            Group = "Tactical";
        }

     
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Risk", "Risk"));
            types.Add(new ItemTypeInfo("Calculation", "Calculation"));
            types.Add(new ItemTypeInfo("Scenario", "Scenario"));

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
