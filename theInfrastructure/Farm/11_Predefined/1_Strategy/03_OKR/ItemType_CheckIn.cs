using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_CheckIn : BaseItemType, IItemType
    {
        public ItemType_CheckIn()
        {
            Name = "CheckIn";
            Title = "Check In";
            this.InSubNavigation = true;
            this.Order = 44;
            Description = "";
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
                Column = "CheckIn",
                Title = "Check-In",
                Typ = FieldTyp.TextArea,
                CSS = "col-12",
                OnDisplay = new FieldDisplay(true, false, false, false),
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "OKR",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Objective",
                Title = "Objective",
                Typ = FieldTyp.Select,
                ItemType = "Objective",
                CSS = "col-12 col-lg-6",
                Description = "Wer hat das Check-In vorgenommen?",
            });

            fields.Add(new Field()
            {
                Column = "KeyResult",
                Title = "Key-Result",
                Typ = FieldTyp.Select,
                ItemType = "KeyResult",
                CSS = "col-12 col-lg-6",
                Description = "Wer hat das Check-In vorgenommen?",
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Zuordnung",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Owner",
                Title = "Owner",
                Typ = FieldTyp.User,
                CSS = "col-12 col-lg-6",
                Description = "Wer hat das Check-In vorgenommen?",
            });

            fields.Add(new Field()
            {
                Column = "Datum",
                Title = "Datum",
                Typ = FieldTyp.Date,
                CSS = "col-12 col-lg-6",
                Description = "Datum des Check-In?",
            });

            fields.Add(new Field()
            {
                Column = "-",
                Title = "Einschätzung",
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
                Title = "Wert (aktuell)",
                Typ = FieldTyp.Text,
                CSS = "col-12 col-lg-4",
                Description = "Der aktuelle Wert zum Zeitpunkt des Check-Ins.",
                OnDisplay = new FieldDisplay(true, false, true, false),
            });


            fields.Add(new Field()
            {
                Column = "-",
                Title = "Bewertung",
                Typ = FieldTyp.HR,
                CSS = "col-12"
            });

            fields.Add(new Field()
            {
                Column = "Positiv",
                Title = "Positiv",
                Typ = FieldTyp.TextArea,
                CSS = "col-12 col-lg-6",
                Description = "Was ist positiv.",
                OnDisplay = new FieldDisplay(true, false, false, false),
            });

            fields.Add(new Field()
            {
                Column = "Negativ",
                Title = "Negativ",
                Typ = FieldTyp.TextArea,
                CSS = "col-12 col-lg-6",
                Description = "Was ist negativ.",
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
