using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    public interface IField
    {
        string Column { get; set; }
        FieldTyp Typ { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Placeholder { get; set; }
        string CSS { get; set; }
        string Extension { get; set; }
        bool IsNecessary { get; set; }
        string DefaultValue { get; set; }

        FieldDisplay OnDisplay { get; set; }
        FieldFunction Funktionen { get; set; }
        DeviceDisplay OnDevice { get; set; }
        bool IsEditable { get; set; }

        // Formatierung 
        TextFormat Formatierung { get; set; }

        // CheckBox 
        string TrueText { get; set; } 
        string FalseText { get; set; }

        // DropDown List 
        string ItemType { get; set; } // ItemType zum laden 
        List<string> Items { get; set; } // vordefinierte Items in der DDL 
        AssociationTyp Association { get; set; }
        // Condition 
        FieldCondition Condition { get; set; } // Definiert eine Field Wert Condition um dieses Feld einzublenden 
    }

    public class Field : IField
    {
        public Field()
        { }
        public FieldTyp Typ { get; set; } = FieldTyp.Text;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Placeholder { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
        public bool IsNecessary { get; set; } = false;
        public string DefaultValue { get; set; } = string.Empty;

        public string CSS { get; set; } = "col";
        public string Extension { get; set; } = string.Empty;
        public DeviceDisplay OnDevice { get; set; } = DeviceDisplay.NULL;
        public FieldDisplay OnDisplay { get; set; } = new();
        public FieldFunction Funktionen { get; set; } = new();

        public bool IsEditable { get; set; } = true;
        public TextFormat Formatierung { get; set; } = TextFormat.NULL;

        // CheckBox 
        public string TrueText { get; set; } = string.Empty;
        public string FalseText { get; set; } = string.Empty;

        // DropDown List 
        public string ItemType { get; set; } = string.Empty;
        public List<string> Items { get; set; } = new();
        public AssociationTyp Association { get; set; } = AssociationTyp.Association;
        public static List<IField> DefaultFields(DefaultFieldTypes typ)
        {
            List<IField> Fields = new();

            if (typ == DefaultFieldTypes.Performance)
            {
                Fields.Add(new Field()
                {
                    Title = "Status", Description = "Mit dem Status kennzeichnen Sie den aktuellen Bearbeitungszustand einer Aufgabe, wodurch Sie den Arbeitsfluss im gesamten Team transparent strukturieren, Übergaben nahtlos gestalten und operative Blockaden im Unternehmensablauf sofort sichtbar machen.",
                    Typ = FieldTyp.Status,
                    DefaultValue = "neu",
                    Column = "Status", CSS = " col-12 col-md-6 col-lg-4 ",
                    Funktionen = new FieldFunction(true, true),
                    OnDevice = DeviceDisplay.Tablet
                });
                Fields.Add(new Field() 
                { 
                    Title = "Fortschritt", Description = "Mit dem Fortschritt dokumentieren und verfolgen Sie den aktuellen Erfüllungsgrad einer Aufgabe, wodurch Sie Abweichungen vom Zeitplan frühzeitig erkennen, die Transparenz im Team erhöhen und eine verlässliche Grundlage für die Kapazitäts- und Terminplanung im Unternehmen schaffen.", 
                    Typ = FieldTyp.Progress, 
                    DefaultValue = "0",
                    Column = "Progress", CSS = " col-12 col-md-6 col-lg-4 ",
                    Funktionen = new FieldFunction(true, true),
                    OnDevice = DeviceDisplay.Tablet 
                });
                Fields.Add(new Field() 
                { 
                    Title = "Priorität", Description = "Mit der Priorität legen Sie die Dringlichkeit und Wichtigkeit einer Aufgabe fest, wodurch Ihr Team Ressourcen gezielt auf kritische Arbeitsschritte konzentriert, Engpässe frühzeitig vermeidet und die produktive Gesamtleistung des Unternehmens maximiert.", 
                    Typ = FieldTyp.Priority, 
                    DefaultValue = "ausgeglichen",
                    Column = "Prio", CSS = " col-12 col-md-6 col-lg-4 ",
                    Funktionen = new FieldFunction(true, true),
                    OnDevice = DeviceDisplay.Tablet });
            }

            if (typ == DefaultFieldTypes.Description)
            {
                Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
                Fields.Add(new Field() 
                { 
                    Title = "Beschreibung", 
                    Typ = FieldTyp.TextArea, 
                    Column = "Description", CSS = " col-12 col-md-12 col-lg-12 ", 
                    OnDevice = DeviceDisplay.Desktop,
                    OnDisplay = new FieldDisplay(true, true, false, false),
                });
            }

            if (typ == DefaultFieldTypes.File)
            {
                
                Fields.Add(new Field() { Title = "Datei", Typ = FieldTyp.HR });
                Fields.Add(new Field() { Title = "Pfad", Typ = FieldTyp.TextBlock, 
                    Column = "FullFilePath",  CSS = " col-12 col-md-8 col-lg-9 ", 
                    OnDisplay = new FieldDisplay(true, true, false,false),
                    OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Erweiterung", Typ = FieldTyp.TextBlock, 
                    Column = "FileExtension",  CSS = " col-12 col-md-4 col-lg-3 ", OnDevice = DeviceDisplay.Desktop });

               
                
                Fields.Add(new Field() { Title = "Größen", Typ = FieldTyp.HR });
               Fields.Add(new Field() { 
                    Title = "Size in Byte", Typ = FieldTyp.TextBlock, Column = "FileSizeInByte", 
                    CSS = " col-12 col-md-4 col-lg-4 ", 
                    Extension = "Byte",
                    Formatierung = TextFormat.Byte,
                    OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { 
                    Title = "Size in KB", Typ = FieldTyp.TextBlock, Column = "FileSizeInKB", 
                    CSS = " col-12 col-md-4 col-lg-4 ", 
                    Extension = "KB",
                    Formatierung = TextFormat.KB,
                    OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { 
                    Title = "Size in MB", Typ = FieldTyp.TextBlock, Column = "FileSizeInMB", 
                    CSS = " col-12 col-md-4 col-lg-4 ", 
                    Extension = "MB",
                    Formatierung = TextFormat.MB,
                    OnDevice = DeviceDisplay.Desktop });

                Fields.Add(new Field() { Title = "Inhalt", Typ = FieldTyp.HR });
                Fields.Add(new Field()
                {
                    Title = "MimeType",
                    Typ = FieldTyp.TextBlock,
                    Column = "MimeType",
                    CSS = " col-12 col-md-4 col-lg-4 ",
                    OnDevice = DeviceDisplay.Desktop
                });
                Fields.Add(new Field()
                {
                    Title = "IsText",
                    Typ = FieldTyp.TextBlock,
                    Column = "IsText",
                    CSS = " col-12 col-md-4 col-lg-4 ",
                    OnDevice = DeviceDisplay.Desktop
                });
                Fields.Add(new Field()
                {
                    Title = "IsBinary",
                    Typ = FieldTyp.TextBlock,
                    Column = "IsBinary",
                    CSS = " col-12 col-md-4 col-lg-4 ",
                    OnDevice = DeviceDisplay.Desktop
                });

            }

            if (typ == DefaultFieldTypes.Appointment)
            {
                Fields.Add(new Field() 
                { 
                    Title = "Agenda", Typ = FieldTyp.TextArea, 
                    Column = "Agenda", CSS = " col-12 col-md-6 col-lg-6 ", 
                    OnDisplay = new FieldDisplay(true, true, false, false) });


                Fields.Add(new Field()
                {
                    Title = "Teilnehmer",
                    Typ = FieldTyp.Select,
                    Association = AssociationTyp.Children,
                    ItemType = "User",
                    Column = "Teilnehmer",
                    CSS = " col-12 col-md-6 col-lg-6 ",
                    OnDisplay = new FieldDisplay(true, true, true, false)
                });

                Fields.Add(new Field() { Title = "Termin", Typ = FieldTyp.HR, CSS = " col-12 col-md-12 col-lg-12 " });
                Fields.Add(new Field() { Title = "Datum", Typ = FieldTyp.Date, Column = "Date", CSS = " col-12 col-md-12 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Start", Typ = FieldTyp.Time, Column = "Start", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Ende", Typ = FieldTyp.Time, Column = "Ende", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            }

            if (typ == DefaultFieldTypes.Reminder)
            {

                Fields.Add(new Field() 
                { 
                    Title = "Aktiv", Typ = FieldTyp.CheckBox, TrueText="aktiv", FalseText="inaktiv", 
                    Column = "IsActive", CSS = " col-12 col-md-12 col-lg-12 ", OnDevice = DeviceDisplay.Desktop 
                });
                Fields.Add(new Field()
                {
                    Title = "Intervall",
                    Typ = FieldTyp.,
                    TrueText = "aktiv",
                    FalseText = "inaktiv",
                    Column = "IsActive",
                    CSS = " col-12 col-md-12 col-lg-12 ",
                    OnDevice = DeviceDisplay.Desktop
                });

                Fields.Add(new Field() { Title = "Erinnerung", Typ = FieldTyp.HR, CSS = " col-12 col-md-12 col-lg-12 " });
                Fields.Add(new Field() { Title = "Datum", Typ = FieldTyp.Date, Column = "Date", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Uhrzeit", Typ = FieldTyp.Time, Column = "Time", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Ende", Typ = FieldTyp.Date, Column = "Stop", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            }


            if (typ == DefaultFieldTypes.Checklist)
            {
                Fields.Add(new Field()
                {
                    Title = "Aktiv",
                    Typ = FieldTyp.CheckBox,
                    Column = "IsChecked",
                    DefaultValue = "false",
                    OnDisplay = new FieldDisplay(false, false, false, false),
                    CSS = " col-12 col-md-4 col-lg-4 "
                });
            }

            return Fields;
        }

        // Condition 
        public FieldCondition Condition { get; set; } = new();
    }

    public class FieldCondition
    {
        public FieldCondition()
        {
            
        }
        public FieldCondition(string Field, string Value, string Condition = "==", string Typ = "Show")
        {
            this.Field = Field;
            this.Value = Value;
            this.Condition = Condition;
            this.Typ = Typ;
        }
        // Bedingung um ein anderes Feld einzublenden 
        public string Field { get; set; } = string.Empty; // Definiert das Feld 
        public string Value { get; set; } = string.Empty; // Definiert den Value 
        public string Condition { get; set; } = "==";
        public string Typ { get; set; } = "Show"; // Show // Hide 

    }

    public class FieldFunction
    {
        public FieldFunction(bool isFilterable = false, bool isSortable = false)
        {
            IsFilterable = isFilterable;
            IsSortable = isSortable;
        }
        public bool IsFilterable { get; set; } = false;
        public bool IsSortable { get; set; } = false;
    }
    public class FieldDisplay
    {
        public FieldDisplay(bool edit = true,  bool add = true, 
                                bool table = true,  bool related = false)
        {
            Edit = edit;
            Add = add;
            Table = table;
            Related = related;
        }
        public bool Edit { get; set; } = true;
        public bool Add { get; set; } = true;
        public bool Table { get; set; } = true;
        public bool Related { get; set; } = false;
    }

}
