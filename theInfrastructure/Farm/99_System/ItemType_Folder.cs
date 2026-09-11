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
