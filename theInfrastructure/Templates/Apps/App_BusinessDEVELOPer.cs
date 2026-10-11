using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_BusinessDEVELOPer : TemplateApp, ITemplateApp
    {
        public TApp_BusinessDEVELOPer()
        {
            ID = "BusinessDEV";
            Name = "BusinessDEVELOPer";
            Title = "the Business DEVELOPer";
            Group = "Strategy";
        }


        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Business Case", "Business Case"));
            types.Add(new ItemTypeInfo("Indicator", "Indicator"));
            types.Add(new ItemTypeInfo("Market", "Market"));
            types.Add(new ItemTypeInfo("GeoArea", "GeoArea"));
            types.Add(new ItemTypeInfo("Topic", "Topic"));

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

            if (ItemType == "Business Case")
            {
                fields.Add(new FieldInfo("Value", "Value", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Info", "Informationen", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Kategorie", "Kategorie", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Zielgruppe", "Zielgruppe", FieldTyp.Text, " col-12 col-lg-4"));
        
                fields.Add(new FieldInfo("Zustand", "Zustand", FieldTyp.HR, " col-12"));
                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
            }

            if (ItemType == "Indicator")
            {
                fields.Add(new FieldInfo("Beschreibung", "Inhalt", FieldTyp.TextArea, " col-12 "));

                fields.Add(new FieldInfo("Info", "Informationen", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Kategorie", "Kategorie", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Thema", "Thema", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Datum", "Datum", FieldTyp.Date, " col-12 col-lg-4"));

                fields.Add(new FieldInfo("Wert", "Wert", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, " col-12 col-lg-6"));
                fields.Add(new FieldInfo("Wert", "Wert", FieldTyp.Text, " col-12 col-lg-6"));
            }

            if (ItemType == "Market")
            {
           
            }

            if (ItemType == "GeoArea")
            {

            }

            if (ItemType == "Topic")
            {

            }

            return fields;
        }
    }
}
