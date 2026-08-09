using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Template : BaseItemType, IItemType
    {
        public ItemType_Template(string Name)
        {
            this.Name = Name;
        }
    }
}
