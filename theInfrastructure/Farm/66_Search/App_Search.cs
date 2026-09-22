using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Search : BaseApp, IApp
    {
        public App_Search()
        { 
            Name = "System";
            IsNavigation = false;

        }

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Query());
            ItemTypes.Add(new ItemType_SearchFilter());
            

            return ItemTypes;
        }
    }
}
