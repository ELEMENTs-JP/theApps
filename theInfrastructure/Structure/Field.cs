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

        DeviceDisplay OnDevice { get; set; }
        bool OnTable { get; set; } 
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
        public bool OnTable { get; set; } = true;

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
                    Title = "Status", Description = "definiert den Status",
                    Typ = FieldTyp.Status,
                    DefaultValue = "neu",
                    Column = "Status", CSS = " col-12 col-md-6 col-lg-4 ",
                    OnDevice = DeviceDisplay.Tablet
                });
                Fields.Add(new Field() 
                { 
                    Title = "Fortschritt", Description = "legt den Fortschritt fest", 
                    Typ = FieldTyp.Progress, 
                    DefaultValue = "0",
                    Column = "Progress", CSS = " col-12 col-md-6 col-lg-4 ", 
                    OnDevice = DeviceDisplay.Tablet 
                });
                Fields.Add(new Field() 
                { 
                    Title = "Priorität", Description = "legt die Priorität fest", 
                    Typ = FieldTyp.Priority, 
                    DefaultValue = "ausgeglichen",
                    Column = "Prio", CSS = " col-12 col-md-6 col-lg-4 ", 
                    OnDevice = DeviceDisplay.Tablet });
            }

            if (typ == DefaultFieldTypes.Description)
            {
                Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
                Fields.Add(new Field() 
                { 
                    Title = "Beschreibung", 
                    Typ = FieldTyp.TextArea, 
                    Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", 
                    OnDevice = DeviceDisplay.Desktop,
                    OnTable = false,
                });
            }

            if (typ == DefaultFieldTypes.File)
            {
                Fields.Add(new Field() { Title = "Typisierung", Typ = FieldTyp.HR });
                Fields.Add(new Field() { Title = "Erweiterung", Typ = FieldTyp.TextBlock, Column = "FileExtension", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

                Fields.Add(new Field() { Title = "Größen", Typ = FieldTyp.HR });
                // Fields.Add(new Field() { Title = "Pfad", Typ = FieldTyp.Text, Column = "FullFilePath", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
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

            }

            if (typ == DefaultFieldTypes.Appointment)
            {
                Fields.Add(new Field() { Title = "Datum", Typ = FieldTyp.Date, Column = "Date", CSS = " col-12 col-md-12 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Start", Typ = FieldTyp.Time, Column = "Start", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Ende", Typ = FieldTyp.Time, Column = "Ende", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
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
}
