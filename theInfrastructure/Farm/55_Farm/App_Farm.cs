using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Farm : BaseApp, IApp
    {
        public App_Farm()
        { 
            Name = "Farm";
            IsNavigation = false;
        }
        public override async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new List<IItemType>();

            // ItemTypes 
            ItemTypes.Add(new ItemType_App());
            ItemTypes.Add(new ItemType_ItemType());
            ItemTypes.Add(new ItemType_Field());
            ItemTypes.Add(new ItemType_Page());
            ItemTypes.Add(new ItemType_Slot());
            ItemTypes.Add(new ItemType_Control());

            return ItemTypes;
        }
    }

}
