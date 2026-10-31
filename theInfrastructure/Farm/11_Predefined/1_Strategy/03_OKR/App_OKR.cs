using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_OKR : BaseApp, IApp
    {
        public App_OKR()
        { 
            Name = "OKR";
            Title = "the OKRler";
            Group = "Strategy";
            IsActive = true;
            IsNavigation = true;
        }

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp typ = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Objective());
            ItemTypes.Add(new ItemType_KeyResult());
            ItemTypes.Add(new ItemType_Alignment());
            ItemTypes.Add(new ItemType_CheckIn());

            return ItemTypes;
        }
    }
}
