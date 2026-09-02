using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Image : BaseItemType, IItemType
    {
        public ItemType_Image()
        {
            Name = "Image";
            Title = "Bilder";
            Typ = ItemTypeTyp.Image;
        }

        public override async Task<List<IField>> GetFields(string view = "")
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
