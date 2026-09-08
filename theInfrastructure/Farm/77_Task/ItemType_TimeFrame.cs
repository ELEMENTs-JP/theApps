using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_TimeFrame : BaseItemType, IItemType
    {
        public ItemType_TimeFrame()
        {
            Name = "TimeFrame";
            Title = "Zyklus";
            this.InSubNavigation = false;
            Description = "Ein klarer Timeframe bringt Struktur in die Unternehmensplanung: Er richtet Budgets, Teams und Fristen gezielt aufeinander aus, damit Projekte im zeitlichen und finanziellen Rahmen bleiben. Das schafft Verlässlichkeit, schützt vor unnötigen Kosten und sorgt dafür, dass aus Plänen pünktlich erfolgreiche Ergebnisse werden.";
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

            // Termin 
            Fields.Add(new Field() { Title = "Jahr", Typ = FieldTyp.Year, Column = "Year", CSS = " col-12 col-md-12 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Start", Typ = FieldTyp.Date, Column = "Start", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });
            Fields.Add(new Field() { Title = "Ende", Typ = FieldTyp.Date, Column = "Ende", CSS = " col-12 col-md-6 col-lg-4 ", OnDevice = DeviceDisplay.Desktop });

            Fields.Add(new Field() { Title = "Budgetierung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Budget", Typ = FieldTyp.Money, Column = "Budget", CSS = " col-12 col-md-12 col-lg-12", OnDevice = DeviceDisplay.Desktop });

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
