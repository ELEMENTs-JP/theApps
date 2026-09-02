using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Audio : BaseItemType, IItemType
    {
        public ItemType_Audio()
        {
            Name = "Audio";
            Title = "Audio";
            Typ = ItemTypeTyp.Audio;
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
