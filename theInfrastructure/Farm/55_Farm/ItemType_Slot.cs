using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Slot : BaseItemType, IItemType
    {
        public ItemType_Slot()
        {
            Name = "Slot";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            // ItemTypes 
            List<IItemType>  ItemTypes = new List<IItemType>();
 
            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Typ", Typ = FieldTyp.Text, Column = "Typ", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Zone", Typ = FieldTyp.Text, Column = "Zone", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Sortierung", Typ = FieldTyp.Text, Column = "Sort", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
