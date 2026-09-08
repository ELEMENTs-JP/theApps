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
            }

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Aufgabe",
                Typ = FieldTyp.TextArea,
                Column = "Description",
                CSS = " col-12 col-md-6 col-lg-12 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(false, false, false, false),
            });


            Fields.Add(new Field() { Title = "Assignment", Typ = FieldTyp.HR });
            Fields.Add(new Field()
            {
                Title = "Owner", Description= "Mit dem Owner legen Sie die eindeutige fachliche und operative Verantwortung für eine Aufgabe fest, wodurch Sie Unklarheiten bei Zuständigkeiten vermeiden, gezielte Rückfragen im Team ermöglichen und die verbindliche Umsetzung unternehmerischer Ziele sicherstellen.",
                Typ = FieldTyp.Select,
                Association = AssociationTyp.Children,
                ItemType = "User",
                Column = "Owner",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field()
            {
                Title = "Projekt",
                Typ = FieldTyp.Text,
                Column = "Projekt",
                CSS = " col-12 col-md-4 col-lg-4 ",
                IsNecessary = true,
                Funktionen = new FieldFunction(true, true),
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field() { 
                Title = "Aktiv", Typ = FieldTyp.CheckBox, Column = "IsActive",
                DefaultValue = "true",
                TrueText="Aktiv", FalseText="Inaktiv",
                CSS = " col-12 col-md-4 col-lg-4 " });

            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Performance));

      

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);

            // Default Values 
            List<IField> fields = await GetFields();
            foreach (IField field in fields.Where(se => !string.IsNullOrEmpty(se.DefaultValue)))
            {
                if (string.IsNullOrEmpty(args.Item[field.Column].ToSecureString()))
                { 
                    args.Item[field.Column] = field.DefaultValue;
                }
            }
            
            // Update 
            await args.SqlService.Update(args.Item);
            
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
