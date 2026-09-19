using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_DOCSTer : TemplateApp, ITemplateApp
    {
        public TApp_DOCSTer()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("DOCSTer", "the DOCSTer");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("FileSet", "FileSet"));

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
