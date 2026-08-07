using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class ItemTypeBuilder
    {
        public ItemTypeBuilder() { }

        public static ItemTypeBuilder Create() { return new ItemTypeBuilder(); }

        public IItemType BuildITemType(string Name)
        {
            switch (Name)
            {
                case "App":
                    {
                        return new ItemType_App();
                    }
                case "ItemType":
                    {
                        return new ItemType_ItemType();
                    }
                case "Task":
                    {
                        return new ItemType_Task();
                    }
                default:
                    {
                        return new ItemType_Task();
                    }
            }
        }
    }
}
