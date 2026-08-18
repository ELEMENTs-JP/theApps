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

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
