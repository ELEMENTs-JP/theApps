using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_CUSTOMer : TemplateApp, ITemplateApp
    {
        public TApp_CUSTOMer()
        {
            ID = "CRM";
            Name = "CUSTOMer";
            Title = "the CUSTOMer";
            Group = "Operational";
        }


  
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Customer", "Customer"));
            types.Add(new ItemTypeInfo("Activity", "Activity"));
            types.Add(new ItemTypeInfo("Demand", "Demand"));
            types.Add(new ItemTypeInfo("Contract", "Contract"));

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
