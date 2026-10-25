using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_PRODUCT : TemplateApp, ITemplateApp
    {
        public TApp_PRODUCT()
        {
            ID = "PRODUCT";
            Name = "PRODUCT";
            Title = "the PRODUCTler";
            Group = "Operation";
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
            types.Add(new ItemTypeInfo("FocusGroup", "Focus Group"));
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

            if (ItemType == "Product")
            {
                fields.Add(new FieldInfo("Value", "Value", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("USP", "USP", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Kategorie", "Kategorie", FieldTyp.Text, " col-12 col-lg-4"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("InScope", "In-Scope", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("OutOfScope", "Out-of-Scope", FieldTyp.TextArea, "col-12 col-lg-6 "));

                fields.Add(new FieldInfo("Launch", "Launch", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Datum", "Launch", FieldTyp.Date, "col-6 "));
                fields.Add(new FieldInfo("IsPublic", "Öffentlich", FieldTyp.CheckBox, "col-6 "));

            }

            if (ItemType == "Feature")
            {
                fields.Add(new FieldInfo("Value", "Value", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Ideas", "Ideen", FieldTyp.TextArea, " col-12 col-lg-6"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Kategorie", "Kategorie", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Component", "Component", FieldTyp.Text, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("InScope", "In-Scope", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("OutOfScope", "Out-of-Scope", FieldTyp.TextArea, "col-12 col-lg-6 "));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
      

                fields.Add(new FieldInfo("Launch", "Launch", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Datum", "Launch", FieldTyp.Date, "col-6 "));
                fields.Add(new FieldInfo("IsPublic", "Öffentlich", FieldTyp.CheckBox, "col-6 "));

            }

            if (ItemType == "Persona")
            {
                fields.Add(new FieldInfo("Name", "Name", FieldTyp.Text, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Age", "Alter", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Sprache", "Sprache", FieldTyp.Text, " col-12 col-lg-4"));

                fields.Add(new FieldInfo("Summary", "Summary", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Summary", "Description", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Goal", "Goal", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Motivation", "Motivation", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Values", "Values", FieldTyp.TextArea, "col-12 col-lg-6 "));

                fields.Add(new FieldInfo("GP", "Gains & Pains", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Gains", "Gains", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Pains", "Pains", FieldTyp.TextArea, "col-12 col-lg-6 "));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));

                fields.Add(new FieldInfo("Persona", "Persona", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Personality", "Persönlichkeit", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Technologie", "Technologie", FieldTyp.TextArea, "col-12 col-lg-6 "));
            }

            if (ItemType == "JTBD")
            {
                fields.Add(new FieldInfo("Content", "Beschreibung", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Gains", "Gains", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Pains", "Pains", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Job", "Job", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("C-Jobs", "Consumption Jobs", FieldTyp.TextArea, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("R-Jobs", "Related Jobs", FieldTyp.TextArea, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("E-Jobs", "Emotional Jobs", FieldTyp.TextArea, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("to-be", "to be", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Define", "1.) Define", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Locate", "2.) Locate", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Prepare", "3.) Prepare", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Confirm", "4.) Confirm", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Execute", "5.) Execute", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Monitoring", "6.) Monitor", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Modify", "7.) Modify", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Conclude", "8.) Conclude", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Done-Outcomes", "Done (Outcomes)", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("AppropriatelyServed", "Appropriately Served", FieldTyp.TextArea, "col-12"));
                fields.Add(new FieldInfo("Overserved", "Overserved", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Irrelevant", "Irrelevant", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("TableStakes", "Table Stakes", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Underserved", "Underserved", FieldTyp.TextArea, "col-12 col-lg-6"));
            }

            if (ItemType == "Release")
            {
                fields.Add(new FieldInfo("Content", "Value", FieldTyp.TextArea, "col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Datum", "Datum", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Build", "Build", FieldTyp.Text, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("ZFT", "Zustand", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Priority", "Priorität", FieldTyp.Priority, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("InScope", "In-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("OutOfScope", "Out-of-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
            }

            if (ItemType == "Version")
            {
                fields.Add(new FieldInfo("Content", "Value", FieldTyp.TextArea, "col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Datum", "Datum", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Build", "Build", FieldTyp.Text, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("ZFT", "Zustand", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Priority", "Priorität", FieldTyp.Priority, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("InScope", "In-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("OutOfScope", "Out-of-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
            }

            if (ItemType == "Edition")
            {
                fields.Add(new FieldInfo("Content", "Value", FieldTyp.TextArea, "col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Datum", "Datum", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Build", "Build", FieldTyp.Text, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("ZFT", "Zustand", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Priority", "Priorität", FieldTyp.Priority, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Scope", "Scope", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("InScope", "In-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("OutOfScope", "Out-of-Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
            }

            if (ItemType == "Idea")
            {
                fields.Add(new FieldInfo("Content", "Beschreibung", FieldTyp.TextArea, "col-12 col-lg-9"));
                fields.Add(new FieldInfo("Rating", "Rating", FieldTyp.Text, "col-12 col-lg-3"));

                fields.Add(new FieldInfo("Typisierung", "Typisierung", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Herkunft", "Herkunft", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Datum", "Aufnahme", FieldTyp.Date, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Herkunft", "Herkunft", FieldTyp.Text, "col-12 col-lg-6"));
            }

            if (ItemType == "FocusGroup")
            {
                fields.Add(new FieldInfo("Content", "Beschreibung", FieldTyp.TextArea, "col-12"));
                fields.Add(new FieldInfo("Owner", "Betreuer", FieldTyp.User, "col-12 col-lg-5"));
                fields.Add(new FieldInfo("Atmosphere", "Stimmung", FieldTyp.Text, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Size", "Teilnehmer", FieldTyp.Integer, "col-12 col-lg-3"));
            }

            return fields;
        }
    }
}
