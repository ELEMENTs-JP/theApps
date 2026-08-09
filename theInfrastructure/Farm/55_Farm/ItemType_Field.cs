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
            Init();
        }
        public List<IField> Fields { get; set; } = new();

        public override void Init()
        {
            base.Init(); 

            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR  });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });
        }
    }
}
