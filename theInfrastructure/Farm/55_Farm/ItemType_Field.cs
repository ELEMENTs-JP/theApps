using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Field : BaseItemType, IItemType
    {
        public ItemType_Field()
        {
            Name = "Field";
            InSubNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new();
            Fields.Add(new Field() { Title = "Typ", Typ = FieldTyp.FieldTyp, Column = "Typ", CSS = " col-12 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12", OnDevice = DeviceDisplay.Desktop });
            
            return Fields;
        }
    }
}
