using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_App : BaseItemType, IItemType
    {
        public ItemType_App()
        {
            Name = "App";
            Init();
        }

        public override async Task Init()
        {
            await base.Init();

            // ItemTypes 
            ItemTypes = new List<IItemType>();
            ItemTypes.Add(new ItemType_ItemType());

            // Fields 
            Fields = new List<IField>();
            Fields.Add(new Field() { Title = "Group", Typ = FieldTyp.Text, Column = "Group", CSS = " col-12 " });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR  });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
        }
    }
}
