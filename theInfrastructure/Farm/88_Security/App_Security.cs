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

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Principal());
            ItemTypes.Add(new ItemType_User());
            ItemTypes.Add(new ItemType_Permission());

            return ItemTypes;
        }
    }
}
