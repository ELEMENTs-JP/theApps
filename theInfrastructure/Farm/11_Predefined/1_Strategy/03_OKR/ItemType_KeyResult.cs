using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_KeyResult : BaseItemType, IItemType
    {
        public ItemType_KeyResult()
        {
            Name = "KeyResult";
            Title = "Key Result";
            this.InSubNavigation = true;
            this.Order = 22;
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
                CSS = "col-12 col-lg-8",
                Description = "Wie sieht das erwartete Ergebnis dieses Ziels aus?",
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
                Column = "-",
                Title = "Kennzahl",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Typ",
                Title = "Typ",
                Typ = FieldTyp.DropDown,
                CSS = "col-12 col-lg-4",
                Items = new List<string>() { "Prozentual", "Numerisch", "Binär", "Ergebnis" },
                Description = "Definiert die Art des Key Result.",
            });

            fields.Add(new Field()
            {
                Column = "Kennzahl",
                Title = "Kennzahl",
                Typ = FieldTyp.Text,
                CSS = "col-12 col-lg-4",
                Description = "Allg. Bezeichnung der Kennzahl",
                OnDisplay = new FieldDisplay(true, false, true, false),
            });

            fields.Add(new Field()
            {
                Column = "Value",
                Title = "Wert",
                Typ = FieldTyp.Text,
                CSS = "col-12 col-lg-4",
                Description = "Festlegung des Zielwertes.",
                OnDisplay = new FieldDisplay(true, false, true, false),
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Key Result",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "KR",
                Title = "KR",
                Typ = FieldTyp.KeyResult,
                CSS = "col-12",
                Description = "Key Result",
                OnDisplay = new FieldDisplay(true, false, false, false),
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Konfiguration",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Start",
                Title = "Start",
                Typ = FieldTyp.Date,
                CSS = "col-12 col-lg-4",
                Description = "Beginn der Umsetzung",
            });

            fields.Add(new Field()
            {
                Column = "End",
                Title = "Ziel",
                Typ = FieldTyp.Date,
                CSS = "col-12 col-lg-4",
                Description = "Ende der Umsetzung",
            });

            fields.Add(new Field()
            {
                Column = "Weight",
                Title = "Gewicht",
                Typ = FieldTyp.Gewichtung,
                CSS = "col-12 col-lg-4",
                Description = "Gewichtung",
                
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
