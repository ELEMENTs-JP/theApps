using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_PRODUCT : TemplateApp, ITemplateApp
    {
        public TApp_PRODUCT()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("PRODUCT", "the PRODUCT");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Product", "Product"));
            types.Add(new ItemTypeInfo("Feature", "Feature"));
            types.Add(new ItemTypeInfo("JTBD", "JTBD"));
            types.Add(new ItemTypeInfo("Persona", "Persona"));
            types.Add(new ItemTypeInfo("Story", "Story"));
            types.Add(new ItemTypeInfo("Release", "Release"));
            types.Add(new ItemTypeInfo("Focus Group", "Focus Group"));
            types.Add(new ItemTypeInfo("Idea", "Idea"));
            types.Add(new ItemTypeInfo("Version", "Version"));
            types.Add(new ItemTypeInfo("Edition", "Edition"));

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
