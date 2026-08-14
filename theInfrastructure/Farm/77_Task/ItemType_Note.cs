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
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            return Fields;
        }

    }
}
