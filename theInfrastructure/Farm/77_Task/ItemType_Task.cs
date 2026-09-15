using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Name = "Task";
            this.InSubNavigation = false;
            this.Order = 1;
            Description = "Mit einer Aufgabe erfassen und verwalten Sie alle relevanten operativen Daten Ihres Projekts, wodurch Sie Informationsverluste vermeiden, den manuellen Abstimmungsaufwand im Team spürbar reduzieren und jederzeit eine verlässliche Datenbasis für fundierte unternehmerische Entscheidungen sowie effiziente Prozessabläufe schaffen.";
        }

        // ItemTypes 
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();


            if (ast == AssociationTyp.Children)
            {
                ItemTypes.Add(new ItemType_Checklist());
            }

            if (ast == AssociationTyp.Default)
            {
                ItemTypes.Add(new ItemType_Comment());
                ItemTypes.Add(new ItemType_Note());
                ItemTypes.Add(new ItemType_File());
            }

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Aufgabe", Description ="Beschreibt die inhaltliche Aufgabe.",
                Typ = FieldTyp.TextArea,
                Column = "Description",
                CSS = " col-12 col-md-6 col-lg-6 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });
            Fields.Add(new Field()
            {
                Title = "Ergebnis", Description="Beschreibt das erwartete Ergebnis.",
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
                Association = AssociationTyp.Children,
                ItemType = "User",
                Column = "Owner",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, false, false),

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
                Title = "Due To",
                Typ = FieldTyp.Date,
                Formatierung = TextFormat.Datum,
                Column = "DueTo",
                Description = "Legt den Termin der erwarteten oder notwendigen Fertigstellung fest",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop
            });



            // Performance
            Fields.Add(new Field() { Title = "Performance", Typ = FieldTyp.HR });
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Performance));

            Fields.Add(new Field() { Title = "Estimation", Typ = FieldTyp.HR });
            Fields.Add(new Field()
            {
                Title = "Aufwand (h)",
                Typ = FieldTyp.Integer,
                Column = "Effort",
                Extension = "h",
                Description = "Geschätzter Aufwand in Arbeitsstunden",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, false, false),
            });

            Fields.Add(new Field()
            {
                Title = "Budget",
                Typ = FieldTyp.Money,
                Column = "Budget",
                Description = "Benötigtes Budget in EUR",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, false, false),
            });

            Fields.Add(new Field()
            {
                Title = "Aktiv",
                Typ = FieldTyp.CheckBox,
                Column = "IsActive",
                DefaultValue = "true",
                TrueText = "Aktiv",
                FalseText = "Inaktiv",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, false, false),
            });

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);

            //// Default Values 
            //List<IField> fields = await GetFields();
            //foreach (IField field in fields.Where(se => !string.IsNullOrEmpty(se.DefaultValue)))
            //{
            //    if (string.IsNullOrEmpty(args.Item[field.Column].ToSecureString()))
            //    { 
            //        args.Item[field.Column] = field.DefaultValue;
            //    }
            //}
            
            //// Update 
            //await args.SqlService.Update(args.Item);
            
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
