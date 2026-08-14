using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Trash : BaseItemType, IItemType
    {
        public ItemType_Trash()
        {
            Name = "Trash";
        }
        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            return Fields;
        }
    }
}
