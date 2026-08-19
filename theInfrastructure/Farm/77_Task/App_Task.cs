using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Task : BaseApp, IApp
    {
        public App_Task()
        { 
            Name = "Task";
            IsNavigation = false;
        }

        public override async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Note());
            ItemTypes.Add(new ItemType_File());
            ItemTypes.Add(new ItemType_Appointment());
            ItemTypes.Add(new ItemType_Task());

            return ItemTypes;
        }
    }
}
