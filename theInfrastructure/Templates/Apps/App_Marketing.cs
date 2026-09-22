using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_Marketing : TemplateApp, ITemplateApp
    {
        public TApp_Marketing()
        {
            ID = "MARKETING";
            Name = "Marketing";
            Title = "the MARKETING";
            Group = "Operational";
        }

    
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Brand", "Brand"));
            types.Add(new ItemTypeInfo("Claim", "Claim"));
            types.Add(new ItemTypeInfo("Campagin", "Campagin"));
            types.Add(new ItemTypeInfo("Target Group", "Target Group"));
            types.Add(new ItemTypeInfo("Competitor", "Competitor"));
            types.Add(new ItemTypeInfo("Alternative", "Alternative"));

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
