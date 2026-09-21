using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_TESTer : TemplateApp, ITemplateApp
    {
        public TApp_TESTer()
        {
            
        }

        public override AppInfo GetApp()
        {
            return new AppInfo("TESTer", "the TESTer");
        }
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Test Concept", "Test Concept"));
            types.Add(new ItemTypeInfo("Test Data", "Test Data"));
            types.Add(new ItemTypeInfo("Test Environment", "Test Environment"));
            types.Add(new ItemTypeInfo("Test Case", "Test Case"));
            types.Add(new ItemTypeInfo("Test Step", "Test Step"));
            types.Add(new ItemTypeInfo("Test Run", "Test Run"));
            types.Add(new ItemTypeInfo("Test Result", "Test Result"));

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
