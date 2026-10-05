using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{


    public class TApp_Strategyzer : TemplateApp, ITemplateApp
    {
        public TApp_Strategyzer()
        {
            ID = "STRATEGY";
            Name = "STRATEGYzer";
            Title = "the STRATEGYzer";
            Group = "Strategy";
        }

  
        public override List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();
            
            types.Add(new ItemTypeInfo("Geschäftsfeld", "Geschäftsfeld"));
            types.Add(new ItemTypeInfo("Portfolio", "Portfolio"));
            types.Add(new ItemTypeInfo("Strategy", "Strategy"));
            types.Add(new ItemTypeInfo("Initiative", "Initiative"));
            //types.Add(new ItemTypeInfo("Scorecard", "Business Scorecard"));
            //types.Add(new ItemTypeInfo("KPI", "Key Performance Indicator"));
            //types.Add(new ItemTypeInfo("TBI", "Time Based Indicator"));
            //types.Add(new ItemTypeInfo("SWOT", "SWOT Analysis"));
            //types.Add(new ItemTypeInfo("PESTEL", "PESTEL Analysis"));


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

            if (ItemType == "Geschäftsfeld")
            {
                fields.Add(new FieldInfo("Beschreibung", "Beschreibung", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.TextBlock, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Jahr", "Jahr", FieldTyp.Year, "col-lg-4 col-12"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-lg-4 col-12"));
            }

            if (ItemType == "Portfolio")
            {
                fields.Add(new FieldInfo("Beschreibung", "Beschreibung", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.TextBlock, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Jahr", "Jahr", FieldTyp.Year, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, "col-lg-4 col-12"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-lg-4 col-12"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-lg-4 col-12"));
            }

            if (ItemType == "Strategy")
            {
                fields.Add(new FieldInfo("Positionierung", "Positionierung", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Value", "Value", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("In Scope", "In Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Out of Scope", "Out of Scope", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Business", "Unternehmen", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Market", "Markt", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.TextBlock, " col-12 col-lg-6"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-6"));

                fields.Add(new FieldInfo("Start", "Start", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Ende", "Ende", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Jahr", "Jahr", FieldTyp.Year, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, "col-12 col-lg-4 "));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-12 col-lg-4 "));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
            }

            if (ItemType == "Initiative")
            {
                fields.Add(new FieldInfo("Statement", "Statement", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Target", "Target", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("In Scope", "In Scope", FieldTyp.TextArea, "col-12 col-lg-6"));
                fields.Add(new FieldInfo("Out of Scope", "Out of Scope", FieldTyp.TextArea, "col-12 col-lg-6"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.TextBlock, " col-12 col-lg-6"));
                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-6"));

                fields.Add(new FieldInfo("Start", "Start", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Ende", "Ende", FieldTyp.Date, "col-12 col-lg-4"));
                fields.Add(new FieldInfo("Jahr", "Jahr", FieldTyp.Year, "col-12 col-lg-4"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, "col-12 col-lg-4 "));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, "col-12 col-lg-4 "));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
            }

            return fields;
        }

        public override List<PageInfo> GetPages()
        {
            List<PageInfo> pages = new List<PageInfo>();

            //pages.Add(new PageInfo("Alignment", "Alignment"));
            //pages.Add(new PageInfo("Zielerreichung", "Zielerreichung"));

            return pages;
        }

        public override List<BoardInfo> GetBoards()
        {
            List<BoardInfo> boards = new List<BoardInfo>();

            boards.Add(new BoardInfo("Ansoff Matrix", "Ansoff Matrix", "Ansoff Matrix"));
            boards.Add(new BoardInfo("PESTEL", "PESTEL", "PESTEL"));
            boards.Add(new BoardInfo("SWOT", "SWOT", "SWOT"));

            return boards;
        }
    }
}
