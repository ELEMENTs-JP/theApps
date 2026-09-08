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
            this.IsNavigation = false;
            this.ShowInTaskBarNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Link", Typ = FieldTyp.Url, Column = "Link", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Information", Typ = FieldTyp.TextArea, Column = "Information", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
