using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_OKR : TemplateApp, ITemplateApp
    {
        public TApp_OKR()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("OKR", "the OKR");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Objective", "Objective"));
            types.Add(new ItemTypeInfo("KeyResult", "Key Result"));
            types.Add(new ItemTypeInfo("CheckIn", "Check In"));
            types.Add(new ItemTypeInfo("Alignment", "Alignment"));

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
