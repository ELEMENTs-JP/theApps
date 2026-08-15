using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Archive : BaseItemType, IItemType
    {
        public ItemType_Archive()
        {
            Name = "Archive";
        }
        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.Text, Column = "OriginalItemType", CSS = " col-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
