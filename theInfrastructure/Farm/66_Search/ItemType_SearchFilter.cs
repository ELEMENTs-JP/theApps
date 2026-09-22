using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_SearchFilter : BaseItemType, IItemType
    {
        public ItemType_SearchFilter()
        {
            Name = "SearchFilter";
            Title = "SearchFilter";
            this.Typ = ItemTypeTyp.Item;

            this.Order = 99;

 
            this.InSubNavigation = true;
            this.ShowInTaskBarNavigation = false;
        }

        // ItemTypes 
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            if (ast == AssociationTyp.Children)
            {
                // ItemTypes.Add(new ItemType_Tag());
            }

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "ItemType",
                Typ = FieldTyp.ItemTypeList,
                Column = "ItemType",
                CSS = " col-12 "
            });

            //Fields.Add(new Field()
            //{
            //    Title = "Typ",
            //    Typ = FieldTyp.DropDown,
            //    Items = new List<string>() { "Item", "List" },
            //    Column = "Typ",
            //    CSS = " col-6 ",
            //    OnDevice = DeviceDisplay.Desktop
            //});

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

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
