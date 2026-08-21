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

        DeviceDisplay OnDevice { get; set; }
        bool IsEditable { get; set; }

        // CheckBox 
        string TrueText { get; set; } 
        string FalseText { get; set; }
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
        
        public string CSS { get; set; } = "col";
        public DeviceDisplay OnDevice { get; set; } = DeviceDisplay.NULL;

        public bool IsEditable { get; set; } = true;

        // CheckBox 
        public string TrueText { get; set; } = string.Empty;
        public string FalseText { get; set; } = string.Empty;

        public static List<IField> DefaultFields(DefaultFieldTypes typ)
        {
            List<IField> Fields = new();

            if (typ == DefaultFieldTypes.Performance)
            {
                Fields.Add(new Field()
                {
                    Title = "Status", Description = "definiert den Status",
                    Typ = FieldTyp.Status,
                    Column = "Status", CSS = " col-12 col-md-6 col-lg-4 ",
                    OnDevice = DeviceDisplay.Tablet
                });
                Fields.Add(new Field() 
                { 
                    Title = "Fortschritt", Description = "legt den Fortschritt fest", 
                    Typ = FieldTyp.Progress, 
                    Column = "Progress", CSS = " col-12 col-md-6 col-lg-4 ", 
                    OnDevice = DeviceDisplay.Tablet 
                });
                Fields.Add(new Field() 
                { 
                    Title = "Priorität", Description = "legt die Priorität fest", 
                    Typ = FieldTyp.Priority, 
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
                    OnDevice = DeviceDisplay.Desktop });
            }

            if (typ == DefaultFieldTypes.File)
            {
                // Fields.Add(new Field() { Title = "Pfad", Typ = FieldTyp.Text, Column = "FullFilePath", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Size in Byte", Typ = FieldTyp.Text, Column = "FileSizeInByte", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Size in KB", Typ = FieldTyp.Text, Column = "FileSizeInKB", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Size in MB", Typ = FieldTyp.Text, Column = "FileSizeInMB", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Erweiterung", Typ = FieldTyp.Text, Column = "FileExtension", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            }

            if (typ == DefaultFieldTypes.Appointment)
            {
                Fields.Add(new Field() { Title = "Datum", Typ = FieldTyp.Date, Column = "Date", CSS = " col-12 col-md-12 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Start", Typ = FieldTyp.Time, Column = "Start", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
                Fields.Add(new Field() { Title = "Ende", Typ = FieldTyp.Time, Column = "Ende", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            }

            return Fields;
        }
    }
}
