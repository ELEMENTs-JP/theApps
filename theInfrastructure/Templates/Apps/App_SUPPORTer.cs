using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_SUPPORTer : TemplateApp, ITemplateApp
    {
        public TApp_SUPPORTer()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("SUPPORTer", "the SUPPORTer");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Incident", "Incident"));

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
