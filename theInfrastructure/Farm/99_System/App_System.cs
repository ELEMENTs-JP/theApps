using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_System : BaseApp, IApp
    {
        public App_System()
        { 
            Name = "System";
            IsNavigation = false;

        }

        public override async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Trash());

            return ItemTypes;
        }
    }
}
