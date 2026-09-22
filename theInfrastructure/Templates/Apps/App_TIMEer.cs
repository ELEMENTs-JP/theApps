using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_TIMEr : TemplateApp, ITemplateApp
    {
        public TApp_TIMEr()
        {
            ID = "TIME";
            Name = "TIMEr";
            Title = "the TIMEr";
            Group = "Cross";
        }

     
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("TimeRecord", "TimeRecord"));

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
