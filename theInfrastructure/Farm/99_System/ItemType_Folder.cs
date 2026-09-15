using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_Folder : BaseItemType, IItemType
    {
        public ItemType_Folder()
        {
            Name = "Folder";
            Title = "Folder";
            Typ = ItemTypeTyp.Folder;
            this.InSubNavigation = false;

            this.Order = 7;
            this.ShowInTaskBarNavigation = false;
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
                Title = "Beschreibung",
                Typ = FieldTyp.TextArea,
                Column = "Description",
                CSS = " col-12 col-md-8 col-lg-8 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });



            Fields.Add(new Field()
            {
                Title = "Farbe",
                Typ = FieldTyp.Color,
                Column = "Color",
                DefaultValue = "#ffffff",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDisplay = new FieldDisplay(true, true, true, false)
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
