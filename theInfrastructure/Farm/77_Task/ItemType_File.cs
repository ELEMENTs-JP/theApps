using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_File : BaseItemType, IItemType
    {
        public ItemType_File()
        {
            Name = "File";
            Title = "Dateien";
            Typ = ItemTypeTyp.File;
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Files 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.File));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

            return Fields;
        }

    }
}
