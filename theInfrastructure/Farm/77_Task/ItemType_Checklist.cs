using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Checklist : BaseItemType, IItemType
    {
        public ItemType_Checklist()
        {
            Name = "Checklist";
            this.Order = 12;

            this.InSubNavigation = false;
            this.ShowInTaskBarNavigation = false;
            this.IsNavigation = false;

            this.Typ = ItemTypeTyp.Checklist;

            Description = "";
        }

        // ItemTypes 
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            //if (ast == AssociationTyp.Children)
            //{
            //    ItemTypes.Add(new ItemType_Comment());
            //    ItemTypes.Add(new ItemType_Note());
            //}

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Inhalt",
                Typ = FieldTyp.TextArea,
                Column = "Description",
                CSS = " col-12 col-md-12 col-lg-12 ",
                OnDevice = DeviceDisplay.Desktop,
                OnDisplay = new FieldDisplay(true, true, false, false),
            });

            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Checklist));

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);

            //// Default Values 
            //List<IField> fields = await GetFields();
            //foreach (IField field in fields.Where(se => !string.IsNullOrEmpty(se.DefaultValue)))
            //{
            //    if (string.IsNullOrEmpty(args.Item[field.Column].ToSecureString()))
            //    { 
            //        args.Item[field.Column] = field.DefaultValue;
            //    }
            //}
            
            //// Update 
            //await args.SqlService.Update(args.Item);
            
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
