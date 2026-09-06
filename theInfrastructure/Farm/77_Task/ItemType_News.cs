using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_News : BaseItemType, IItemType
    {
        public ItemType_News()
        {
            Name = "News";
            Title = "Nachrichten";
            Typ = ItemTypeTyp.Information;
            this.InSubNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Information", Typ = FieldTyp.TextArea, Column = "Information", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
