using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Note : BaseItemType, IItemType
    {
        public ItemType_Note()
        {
            Name = "Note";
            Title = "Notizen";
            Typ = ItemTypeTyp.Note;
            this.InSubNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Notiz", Typ = FieldTyp.TextArea, Column = "Note", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
