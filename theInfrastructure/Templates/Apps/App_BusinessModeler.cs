using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_BusinessModeler : TemplateApp, ITemplateApp
    {
        public TApp_BusinessModeler()
        {
            ID = "BusinessMODEL";
            Name = "BusinessMODELer";
            Title = "the Business MODELer";
            Group = "Strategy";
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

            // cons.Add(new ItemTypeConnection("BusinessModel", "Element")); // Parent -> Child 
            return cons;
        }
        public override List<FieldInfo> GetFields(string ItemType)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            if (ItemType == "BusinessModel")
            {
                fields.Add(new FieldInfo("VP", "Value Proposition", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Prognose","Prognose", FieldTyp.Text, "col-lg-4 col-12"));

                fields.Add(new FieldInfo("Datum", "Ziel", FieldTyp.Date, "col-lg-6 col-12"));
                fields.Add(new FieldInfo("Duration", "Dauer (Y)", FieldTyp.Text, "col-lg-6 col-12"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-lg-4 col-12"));
            }

            if (ItemType == "Element")
            {
                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, " col-12 col-lg-6"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-6"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
            }

            return fields;
        }
    }
}
