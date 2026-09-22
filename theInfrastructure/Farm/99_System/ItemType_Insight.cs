using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_Insight : BaseItemType, IItemType
    {
        public ItemType_Insight()
        {
            Name = "Insight";
            Title = "Insight";
            Description = "Definiert einen vielverwendungsfähigen Datentypen der nach Bedarf genutzt wird";
            this.IsNavigation = false;
            this.InSubNavigation = false;
            this.ShowInTaskBarNavigation = false;
            
            this.Order = 99;
        }

        // ItemTypes 
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Description 
            Fields.Add(new Field()
            {
                Title = "Typ",
                Typ = FieldTyp.Text,
                Description = "Typ: z.B. Suche, KPI, Clicks, etc.",
                Column = "Typ",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);

            // RETURN 
            return args.Item;
        }
        public override async Task<IDTO> AfterCreateItem(IItemEventArgs args)
        {
            await base.AfterCreateItem(args);

            // return 
            return args.Item;
        }

    }
}
