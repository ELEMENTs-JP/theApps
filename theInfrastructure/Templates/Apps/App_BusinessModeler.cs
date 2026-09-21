using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_BusinessModeler : TemplateApp, ITemplateApp
    {
        public TApp_BusinessModeler()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("BusinessMODELer", "the Business MODELer");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("BusinessModel", "Business Model"));
            types.Add(new ItemTypeInfo("Element", "Element"));
            
            return types;
        }
        public override List<ItemTypeConnection> GetItemTypeConnections()
        {
            List<ItemTypeConnection> cons = new List<ItemTypeConnection>();

            cons.Add(new ItemTypeConnection("BusinessModel", "Element")); // Parent -> Child 

            return cons;
        }
        public override List<FieldInfo> GetFields(string ItemType)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            if (ItemType == "Template")
            {
                fields.Add(new FieldInfo("Template", "string"));
                fields.Add(new FieldInfo("Template", "string"));
            }

            return fields;
        }
    }
}
