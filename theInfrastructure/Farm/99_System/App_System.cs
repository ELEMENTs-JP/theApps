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

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            // ItemTypes 
            ItemTypes.Add(new ItemType_Setting());
            ItemTypes.Add(new ItemType_Trash());
            ItemTypes.Add(new ItemType_Archive());

            ItemTypes.Add(new ItemType_Folder());
            ItemTypes.Add(new ItemType_Tag());
            ItemTypes.Add(new ItemType_Label());

            return ItemTypes;
        }
    }
}
