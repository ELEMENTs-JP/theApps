using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Control : BaseItemType, IItemType
    {
        public ItemType_Control()
        {
            Name = "Control";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes()
        {
            // ItemTypes 
            List<IItemType>  ItemTypes = new List<IItemType>();
 
            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Namespace", Typ = FieldTyp.Text, Column = "Namespace", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Klasse", Typ = FieldTyp.Text, Column = "Klasse", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Zone", Typ = FieldTyp.Text, Column = "Zone", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Reihenfolge", Typ = FieldTyp.Text, Column = "Sort", CSS = " col-12 col-md-6 col-lg-6 ", OnDevice = DeviceDisplay.Desktop });
            
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
