using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_ItemType : BaseItemType, IItemType
    {
        public ItemType_ItemType()
        {
            Name = "ItemType";
            InSubNavigation = false;
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            // ItemTypes 
            List<IItemType> ItemTypes = new List<IItemType>();

            if (ast == AssociationTyp.Children)
            {
                ItemTypes.Add(new ItemType_Field());
            }
            if (ast == AssociationTyp.Default)
            {
                ItemTypes.Add(new ItemType_Task());
            }

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Group",
                Description = "Legt die Gruppe des ItemTyp fest.",
                Typ = FieldTyp.Text,
                Column = "Group",
                CSS = " col-6 "
            });
            Fields.Add(new Field()
            {
                Title = "Typ",
                Description = "Legt den Typ des ItemType fest.",
                Typ = FieldTyp.ItemTypeTyp,
                Column = "ItemTypeTyp",
                CSS = " col-6 "
            });


            Fields.Add(new Field() { Title = "Navigation", Typ = FieldTyp.HR });
            Fields.Add(new Field()
            {
                Title = "Order",
                Description = "Legt die Reihenfolge des ItemTyp fest.",
                Typ = FieldTyp.Integer,
                DefaultValue = "1",
                Column = "Order",
                CSS = " col-4 "
            });
            Fields.Add(new Field()
            {
                Title = "Sub Navigation",
                Description = "Legt fest ob dieser ItemType in der Subnavigation angezeigt wird.",
                Typ = FieldTyp.CheckBox,
                Column = "InSubNavigation",
                CSS = " col-4 "
            });
            // AppItemType 
            Fields.Add(new Field()
            {
                Title = "App ItemType",
                Description = "Legt den App relevanten ItemType fest.",
                Typ = FieldTyp.CheckBox,
                Column = "AppItemType",
                CSS = " col-4 "
            });


            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

        public override async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            await base.OnCreateItem(args);

            // Default Values 
            List<IField> fields = await GetFields();
            foreach (IField field in fields.Where(se => !string.IsNullOrEmpty(se.DefaultValue)))
            {
                if (string.IsNullOrEmpty(args.Item[field.Column].ToSecureString()))
                {
                    args.Item[field.Column] = field.DefaultValue;
                }
            }

            // Update 
            await args.SqlService.Update(args.Item);

            // RETURN 
            return args.Item;
        }
    }
}
