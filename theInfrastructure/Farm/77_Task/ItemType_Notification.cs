using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Notification : BaseItemType, IItemType
    {
        public ItemType_Notification()
        {
            Name = "Notification";
            Title = "Notification";
            Typ = ItemTypeTyp.Item;
            this.Order = 14;
            this.InSubNavigation = false;
            this.IsNavigation = true;
            this.ShowInTaskBarNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Benachrichtigungen", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Bearbeitung", Typ = FieldTyp.CheckBox, Column = "Update", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Löschen", Typ = FieldTyp.CheckBox, Column = "Delete", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field() { Title = "Item", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "GUID", Typ = FieldTyp.TextBlock, Column = "GUID", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.TextBlock, Column = "ItemType", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });


            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Information", Typ = FieldTyp.TextArea, Column = "Information", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
