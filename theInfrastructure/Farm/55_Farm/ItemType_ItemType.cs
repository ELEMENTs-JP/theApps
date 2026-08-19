using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_ItemType : BaseItemType, IItemType
    {
        public ItemType_ItemType()
        {
            Name = "ItemType";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes()
        {
            // ItemTypes 
            List<IItemType>  ItemTypes = new List<IItemType>();
            ItemTypes.Add(new ItemType_ItemType());
            ItemTypes.Add(new ItemType_Field());
            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Group",
                Description = "Legt die Gruppe des ItemTyp fest.",
                Typ = FieldTyp.Text,
                Column = "Group",
                CSS = " col-6 "
            });

            Fields.Add(new Field()
            {
                Title = "Sub Navigation",
                Description = "Legt fest ob dieser ItemType in der Subnavigation angezeigt wird.",
                Typ = FieldTyp.CheckBox,
                Column = "InSubNavigation",
                CSS = " col-3 "
            });

            Fields.Add(new Field()
            {
                Title = "Typ",
                Description = "Legt den Typ des ItemType fest.",
                Typ = FieldTyp.ItemTypeTyp,
                Column = "ItemTypeTyp",
                CSS = " col-3 "
            });

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
