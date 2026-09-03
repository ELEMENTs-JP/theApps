using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Name = "Task";
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Owner",
                Typ = FieldTyp.Select,
                Association = AssociationTyp.Children,
                ItemType = "User",
                Column = "Owner",
                CSS = " col-12 col-md-4 col-lg-4 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field()
            {
                Title = "Projekt",
                Typ = FieldTyp.Text,
                Column = "Projekt",
                CSS = " col-12 col-md-4 col-lg-4 ",
                IsNecessary = true,
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field() { 
                Title = "Aktiv", Typ = FieldTyp.CheckBox, Column = "IsActive",
                DefaultValue = "true",
                TrueText="Aktiv", FalseText="Inaktiv",
                CSS = " col-12 col-md-4 col-lg-4 " });
            
            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Performance));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

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
        public override async Task<IDTO> AfterCreateItem(IItemEventArgs args)
        {
            await base.AfterCreateItem(args);

            // return 
            return args.Item;
        }

    }
}
