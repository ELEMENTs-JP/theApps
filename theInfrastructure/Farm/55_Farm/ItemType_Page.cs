using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Page : BaseItemType, IItemType
    {
        public ItemType_Page()
        {
            Name = "Page";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            // ItemTypes 
            List<IItemType>  ItemTypes = new List<IItemType>();
            ItemTypes.Add(new ItemType_Slot());
            ItemTypes.Add(new ItemType_Control());
            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
