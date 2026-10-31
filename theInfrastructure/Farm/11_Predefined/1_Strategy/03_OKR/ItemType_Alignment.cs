using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Alignment : BaseItemType, IItemType
    {
        public ItemType_Alignment()
        {
            Name = "Alignment";
            Title = "Alignment";
            this.Typ = ItemTypeTyp.Appointment;
            this.InSubNavigation = true;
            this.Order = 33;
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
                Column = "Agenda",
                Title = "Agenda",
                Typ = FieldTyp.TextArea,
                CSS = "col-12",
                Description = "Agenda des Alignment",
            });

            fields.Add(new Field()
            {
                Column = "Moderator",
                Title = "Moderator",
                Typ = FieldTyp.User,
                CSS = "col-12 col-lg-4",
                Description = "Der Moderator des Alignment",
            });

            fields.Add(new Field()
            {
                Column = "Teilnehmer",
                Title = "Teilnehmer",
                Typ = FieldTyp.Select,
                ItemType = "User",
                CSS = "col-12 col-lg-8",
                Description = "Teilnehmer des Alignments",
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
                Column = "Start",
                Title = "Beginn",
                Typ = FieldTyp.DateTime,
                CSS = "col-12 col-lg-6",
                Description = "Beginn des Alignment",
            });

            fields.Add(new Field()
            {
                Column = "End",
                Title = "Ende",
                Typ = FieldTyp.DateTime,
                CSS = "col-12 col-lg-6",
                Description = "Ende des Alignment",
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
