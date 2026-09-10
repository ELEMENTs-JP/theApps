using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Setting : BaseItemType, IItemType
    {
        public ItemType_Setting()
        {
            Name = "Setting";
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() 
            { 
                Title = "Typ", Typ = FieldTyp.DropDown,
                Items = new List<string>() { "Item", "List" },
                Column = "Typ", CSS = " col-6 ", 
                OnDevice = DeviceDisplay.Desktop });
            
            Fields.Add(new Field() 
            { 
                Title = "ItemType", Typ = FieldTyp.ItemTypeList, 
                Column = "ItemType", CSS = " col-6 ", 
                OnDevice = DeviceDisplay.Desktop });


            Fields.Add(new Field()
            {
                Title = "Einstellung",
                Typ = FieldTyp.HR,
                CSS = " col-12 "
            });
            Fields.Add(new Field()
            {
                Title = "Setting",
                Typ = FieldTyp.Text,
                Column = "Setting",
                CSS = " col-6 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field()
            {
                Title = "Value",
                Typ = FieldTyp.Text,
                Column = "Value",
                CSS = " col-6 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field()
            {
                Title = "Personalisierung",
                Typ = FieldTyp.HR,
                CSS = " col-12 "
            });
            Fields.Add(new Field()
            {
                Title = "Level",
                Typ = FieldTyp.DropDown,
                Items = new List<string>() { "Principal", "User" },
                DefaultValue = "Principal",
                Column = "Level",
                CSS = " col-6 ",
                OnDevice = DeviceDisplay.Desktop
            });
            Fields.Add(new Field()
            {
                Title = "User",
                Typ = FieldTyp.Text,
                Column = "User",
                CSS = " col-6 ",
                OnDevice = DeviceDisplay.Desktop
            });
            return Fields;
        }
    }
}
