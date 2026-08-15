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
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Status",
                Description = "definiert den Status",
                Typ = FieldTyp.Status,
                Column = "Status",
                CSS = " col-12 col-md-6 col-lg-4 ",
                OnDevice = DeviceDisplay.Tablet
            });
            Fields.Add(new Field() { Title = "Fortschritt", Description = "legt den Fortschritt fest", Typ = FieldTyp.Progress, Column = "Progress", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Tablet });
            Fields.Add(new Field() { Title = "Priorität", Description = "legt die Priorität fest", Typ = FieldTyp.Priority, Column = "Prio", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Tablet });

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            
            return Fields;
        }

    }
}
