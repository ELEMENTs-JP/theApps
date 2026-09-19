using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Board : BaseItemType, IItemType
    {
        public ItemType_Board()
        {
            Name = "Board";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            // ItemTypes 
            List<IItemType>  ItemTypes = new List<IItemType>();

            //if (ast == AssociationTyp.Children)
            //{ 
            //    ItemTypes.Add(new ItemType_Slot());
            //    ItemTypes.Add(new ItemType_Control());
            //}

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Typisierung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Aktiv", Typ = FieldTyp.CheckBox, Column = "IsActive", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Typ", Typ = FieldTyp.BoardTyp, Column = "Typ", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.ItemTypeList, Column = "ItemType", CSS = " col-12 col-md-4 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
