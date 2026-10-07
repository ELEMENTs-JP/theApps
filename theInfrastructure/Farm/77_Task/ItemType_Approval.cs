using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Approval : BaseItemType, IItemType
    {
        public ItemType_Approval()
        {
            Name = "Approval";
            Icon = Icon.Task;
            this.InSubNavigation = false;
            this.Order = 99;
            Description = "";
        }

        // Views 
        public virtual async Task<List<IEditView>> GetViews(string typ = "")
        {
            List<IEditView> Views = new();

            //Views.Add(new EditView() { Name = "Task", Title = "Task", Typ = "", Description = "" });
            //Views.Add(new EditView() { Name = "Priorization", Title = "Priorization", Typ = "", Description = "" });
            //Views.Add(new EditView() { Name = "Ownership", Title = "Ownership", Typ = "", Description = "" });
            //Views.Add(new EditView() { Name = "DueTo", Title = "Due To", Typ = "", Description = "" });

            return Views;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Sachverhalt", Description ="Beschreibt den inhaltlichen Sachverhalt.",
                Typ = FieldTyp.TextArea,
                Column = "Description",
                CSS = " col-12 col-md-6 col-lg-6 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });
            Fields.Add(new Field()
            {
                Title = "Solution", Description="Beschreibt wie die potenzielle Lösung aussieht.",
                Typ = FieldTyp.TextArea,
                Column = "Result",
                CSS = " col-12 col-md-6 col-lg-6 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });


            Fields.Add(new Field() { Title = "Assignment", Typ = FieldTyp.HR });
            Fields.Add(new Field()
            {
                Title = "Owner", Description= "Legt den Eigentümer der Aufgabe fest.",
                Typ = FieldTyp.Select,
                Association = AssociationTyp.Parents,
                ItemType = "User",
                Column = "Owner",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, true, false),

            });

            Fields.Add(new Field()
            {
                Title = "Assign To",
                Typ = FieldTyp.Select,
                Association = AssociationTyp.Related,
                ItemType = "User",
                Column = "AssignTo",
                Description = "Legt die fachliche und operative Verantwortung fest",
                CSS = " col-12 col-md-4 col-lg-4 "
            });

            Fields.Add(new Field()
            {
                Title = "needed by",
                Typ = FieldTyp.Date,
                Formatierung = TextFormat.Datum,
                Column = "NeedBy",
                Description = "Entscheidung wird bis zu folgendem Termin benötigt",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, true, true),
                OnDevice = DeviceDisplay.Desktop
            });

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);
                 
            // RETURN 
            return args.Item;
        }
        public override async Task<IDTO> AfterCreateItem(IItemEventArgs args)
        {
            await base.AfterCreateItem(args);

            // return 
            return args.Item;
        }

    }
}
