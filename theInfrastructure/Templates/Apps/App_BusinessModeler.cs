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
            
            types.Add(new ItemTypeInfo("Business Model", "Business Model"));
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

            if (ItemType == "Business Model")
            {
                fields.Add(new FieldInfo("VP", "Value Proposition", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.TextBlock, "col-lg-4 col-12"));
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
                fields.Add(new FieldInfo("Value", "Value", FieldTyp.TextArea, "col-12"));

                fields.Add(new FieldInfo("Owner", "Owner", FieldTyp.User, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Typ", "Typ", FieldTyp.Text, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Horizont", "Horizont", FieldTyp.Text, " col-12 col-lg-4"));

                fields.Add(new FieldInfo("Priority", "Priority", FieldTyp.Priority, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Progress", "Fortschritt", FieldTyp.Progress, " col-12 col-lg-4"));
                fields.Add(new FieldInfo("Status", "Status", FieldTyp.Status, "col-12 col-lg-4 "));
                
                fields.Add(new FieldInfo("MH", "Möglichkeiten u. Herausforderungen", FieldTyp.HR, "col-12"));
                fields.Add(new FieldInfo("Moeglichkeiten", "Möglichkeiten", FieldTyp.TextArea, "col-12 col-lg-6 "));
                fields.Add(new FieldInfo("Herausforderungen", "Herausforderungen", FieldTyp.TextArea, "col-12 col-lg-6 "));

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
         
            boards.Add(new BoardInfo("Business Model Canvas", "Business Model Canvas", "Business Model Canvas"));

            return boards;
        }

    }
}
