using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Name = "Task";
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Performance));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

            return Fields;
        }

    }
}
