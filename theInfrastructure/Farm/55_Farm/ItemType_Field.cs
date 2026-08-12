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
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new();
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
            
            return Fields;
        }
    }
}
