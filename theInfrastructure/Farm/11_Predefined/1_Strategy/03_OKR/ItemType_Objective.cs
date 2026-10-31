using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class ItemType_Objective : BaseItemType, IItemType
    {
        public ItemType_Objective()
        {
            Name = "Objective";
            Title = "Objective";
            this.InSubNavigation = false;
            this.Order = 11;
            Description = "";
            this.IsPathElement = true;
        }

        // Views 
        public virtual async Task<List<IEditView>> GetViews(string typ = "")
        {
            List<IEditView> Views = new();



            return Views;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> fields = new List<IField>();

            fields.Add(new Field()
            {
                Column = "Result",
                Title = "Erwartete Ergebnis",
                Typ = FieldTyp.TextArea,
                CSS = "col-12",
                Description = "Wie sieht das erwartete Ergebnis dieses Ziels aus?",
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Terminierung",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Datum",
                Title = "Zieldatum",
                Typ = FieldTyp.Date,
                CSS = "col-12 col-lg-4",
                Description = "Bis wann ist dieses Ziel zu bewirken?",
            });

            fields.Add(new Field()
            {
                Column = "Verantwortlich",
                Title = "Verantwortlich",
                Typ = FieldTyp.User,
                CSS = "col-12 col-lg-4",
                Description = "Wer ist für die Umsetzung verantwortlich?",
            });

            fields.Add(new Field()
            {
                Column = "Ebene",
                Title = "Ebene",
                Typ = FieldTyp.DropDown,
                CSS = "col-12 col-lg-4",
                Description = "Auf welcher Unternehmensebene ist das Ziel angesiedelt?",
                DefaultValue = "Unternehmen",
                Items = new List<string>() { "Unternehmen", "Team", "Personal" },
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Scope",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "In-Scope",
                Title = "In-Scope",
                Typ = FieldTyp.TextArea,
                CSS = "col-12 col-lg-6",
                Description = "Was ist abgedeckt.",
                OnDisplay = new FieldDisplay(true, false, false, false),
            });

            fields.Add(new Field()
            {
                Column = "Out-of-Scope",
                Title = "Out-of-Scope",
                Typ = FieldTyp.TextArea,
                CSS = "col-12 col-lg-6",
                Description = "Was ist nicht abgedeckt.",
                OnDisplay = new FieldDisplay(true, false, false, false),
            });

            return fields;
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
