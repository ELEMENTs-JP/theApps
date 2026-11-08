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
            Typ = AppTyp.Predefined;
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

        public override async Task<List<ComponentDefinition>> GetDashboardComponents(string typ = "")
        {
            List<ComponentDefinition> comps = new List<ComponentDefinition>();

            List<(int, IItemType)> Hierarchy = new List<(int, IItemType)>();
            Hierarchy.Add((1, new ItemType_Objective()));
            Hierarchy.Add((2, new ItemType_KeyResult()));
            //Hierarchy.Add((3, new ItemType_CheckIn()));


            // Test 
            comps.Add(new ComponentDefinition
            {
                Namespace = "theControls",
                ClassName = "OkrList",
                Parameters = new Dictionary<string, object>
                {
                    { "Hierarchy", Hierarchy }
                },
                Name = "OKR List",
                CSS = " col-12 my-2",
                Group = "OKR"

            });

            return comps;
        }
    }
}
