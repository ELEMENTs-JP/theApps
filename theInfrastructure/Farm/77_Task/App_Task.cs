using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_RUNer : BaseApp, IApp
    {
        public App_RUNer()
        { 
            Name = "RUNer";
            IsNavigation = false;
        }

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp typ = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Comment());
            ItemTypes.Add(new ItemType_Note());
            ItemTypes.Add(new ItemType_Image());
            ItemTypes.Add(new ItemType_Audio());
            ItemTypes.Add(new ItemType_File());
            ItemTypes.Add(new ItemType_Link());
            ItemTypes.Add(new ItemType_News());
            ItemTypes.Add(new ItemType_Appointment());
            ItemTypes.Add(new ItemType_TimeFrame());
            ItemTypes.Add(new ItemType_Task());

            return ItemTypes;
        }
    }
}
