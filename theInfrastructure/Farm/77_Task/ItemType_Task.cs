using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Name = "Task";
            Init();
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task Init()
        {
            await base.Init(); 

            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Status", Description = "definiert den Status", Typ = FieldTyp.Text,
                IsEditable = false, Column = "Status", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Tablet });
            Fields.Add(new Field() { Title = "Fortschritt", Description = "legt den Fortschritt fest", Typ = FieldTyp.Text, Column = "Progress", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Tablet });
            Fields.Add(new Field() { Title = "Priorität", Description = "legt die Priorität fest", Typ = FieldTyp.Text, Column = "Prio", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Tablet });
            
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR  });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            
            Fields.Add(new Field() { Title = "Auswahl", Typ = FieldTyp.DropDown, Column = "Auswahl", CSS = " col-12 col-md-6 col-lg-4 " });
            Fields.Add(new Field() { Title = "Done", Typ = FieldTyp.CheckBox, Column = "Done", CSS = " col-12 col-md-6 col-lg-4 " });

            Fields.Add(new Field() { Title = "Zeitliche Zuordnung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Date", Typ = FieldTyp.Date, Column = "Date", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Time", Typ = FieldTyp.Time, Column = "Time", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "DateTime", Typ = FieldTyp.DateTime, Column = "DateTime", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field() { Title = "Zeitliche Zuordnung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Number", Typ = FieldTyp.Number, Column = "Number", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Decimal", Typ = FieldTyp.Decimal, Column = "Decimal", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Integer", Typ = FieldTyp.Integer, Column = "Integer", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field() { Title = "Funktional", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Search", Typ = FieldTyp.Search, Column = "Search", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Color", Typ = FieldTyp.Color, Column = "Color", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Range", Typ = FieldTyp.Range, Column = "Range", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });


        }
    }
}
