using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Security : BaseApp, IApp
    {
        public App_Security()
        { 
            Name = "Security";
            IsNavigation = false;

        }

        public override async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Principal());
            ItemTypes.Add(new ItemType_User());

            return ItemTypes;
        }
    }
}
