using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_SUPPORTer : TemplateApp, ITemplateApp
    {
        public TApp_SUPPORTer()
        {
            ID = "SUPPORT";
            Name = "SUPPORTer";
            Title = "the SUPPORTer";
            Group = "Support";
        }

   
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Issue", "Issue"));
            types.Add(new ItemTypeInfo("Account", "Account"));
            types.Add(new ItemTypeInfo("Piece", "Piece"));
            types.Add(new ItemTypeInfo("Configuration", "Configuration"));
            types.Add(new ItemTypeInfo("Known Error", "Known Error"));
            types.Add(new ItemTypeInfo("Workaround", "Workaround"));
            //types.Add(new ItemTypeInfo("Incident", "Incident"));
            //types.Add(new ItemTypeInfo("Problem", "Problem"));
            //types.Add(new ItemTypeInfo("Change", "Change"));

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
