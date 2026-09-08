using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Favorite : BaseItemType, IItemType
    {
        public ItemType_Favorite()
        {
            Name = "Favorite";
            Title = "Favorite";
            Typ = ItemTypeTyp.Item;
            this.Order = 13;
            this.InSubNavigation = false;
            this.ShowInTaskBarNavigation = false;
            this.IsNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "GUID", Typ = FieldTyp.Text, Column = "GUID", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.Text, Column = "ItemType", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
