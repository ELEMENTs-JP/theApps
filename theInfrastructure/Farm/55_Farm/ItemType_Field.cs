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
            
            Fields.Add(new Field() { 
                Title = "Typ", Typ = FieldTyp.FieldTyp, 
                Description = "Beschreibt den Typ des Feldess",
                Column = "Typ", CSS = " col-12 col-lg-4 ", 
                OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field()
            {
                Title = "Spalte", Typ = FieldTyp.Text,
                Description = "Name der Spalte des Feldes",
                Column = "Column",
                CSS = " col-12 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field()
            {
                Title = "CSS",
                Typ = FieldTyp.Text,
                Description = "Style der Spalte",
                Column = "CSS",
                CSS = " col-12 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12", OnDevice = DeviceDisplay.Desktop });
            
            return Fields;
        }
    }
}
