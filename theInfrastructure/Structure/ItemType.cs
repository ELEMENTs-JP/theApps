using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace theInfrastructure
{

    // Interface 
    public interface IItemType
    {
        string Title { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Group { get; set; }
        Icon Icon { get; set; }
        ItemTypeTyp Typ { get; set; }
        bool IsNavigation { get; set; }
        bool InSubNavigation { get; set; }
        bool ShowInTaskBarNavigation { get; set; }
        int Order { get; set; }

        // ItemType 
        public Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association);

        // Events 
        Task<IDTO> OnCreateItem(IItemEventArgs args);
        Task<IDTO> AfterCreateItem(IItemEventArgs args);
        Task<IDTO> OnUpdateItem(IItemEventArgs args);
        Task<IDTO> OnDeleteItem(IItemEventArgs args);
        Task<IDTO> OnDragDropItem(IItemEventArgs args);

        Task<(IDTO, IDTO)> OnAssignItem(IItemEventArgs args);
        Task<(IDTO, IDTO)> OnRemoveItem(IItemEventArgs args);



        // Fields 
        public Task<List<IField>> GetFields(string view = "");
        public Task<string> GetRelevantPropertyName(RelevantPropertyType typ = RelevantPropertyType.Date);
    }
    public class  BaseItemType : IItemType
    {
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Icon Icon { get; set; } = Icon.NULL;
        public bool IsNavigation { get; set; } = true; // In Hauptnavigation anzeigen 
        public bool InSubNavigation { get; set; } = true; // In untergeordneter Navigation anzeigen 
        public bool ShowInTaskBarNavigation { get; set; } = true; // In der Taskbar unten rechts anzeigen 
        public int Order { get; set; } = 0;
        public ItemTypeTyp Typ { get; set; } = ItemTypeTyp.Item;
        public BaseItemType()
        { 
        
        }

        // ItemTypes 
        public virtual async Task<List<IItemType>> GetItemTypes(
            AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }

        public virtual async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {

            // Year 
            string column = await GetRelevantPropertyName(RelevantPropertyType.Year);
            if (!string.IsNullOrEmpty(column))
            {
                // Value 
                string val = args.Item[column].ToSecureString();

                // Empty 
                if (string.IsNullOrEmpty(val))
                {
                    // set Year 
                    args.Item[column] = DateTime.Now.Year.ToSecureString();

                    // Update 
                    await args.SqlService.Update(args.Item);
                }
            }

            // return 
            return args.Item;
        }
        public virtual async Task<IDTO> AfterCreateItem(IItemEventArgs args)
        {
            // return 
            return args.Item;
        }
        public virtual async Task<IDTO> OnUpdateItem(IItemEventArgs args)
        {
            // return 
            return args.Item;
        }
        public virtual async Task<IDTO> OnDeleteItem(IItemEventArgs args)
        {
            // return 
            return args.Item;
        }
        public virtual async Task<IDTO> OnDragDropItem(IItemEventArgs args)
        {
            // return 
            return args.Item;
        }
        public virtual async Task<(IDTO, IDTO)> OnAssignItem(IItemEventArgs args)
        {
            // return 
            return (args.Item, args.Related);
        }
        public virtual async Task<(IDTO, IDTO)> OnRemoveItem(IItemEventArgs args)
        {
            // return 
            return (args.Item, args.Related);
        }

        // Fields 
        public virtual async Task<List<IField>> GetFields(string view = "")
        {
            List<IField> Fields = new();

            return Fields;
        }
        public async Task<string> GetRelevantPropertyName(RelevantPropertyType typ = RelevantPropertyType.Date)
        {

            string prop = string.Empty;

            List<IField> fields = await this.GetFields();

            // Year 
            if (typ == RelevantPropertyType.Year)
            {
                // 1. Prio = Generell die Frage nach einem Datum 
                IField? f = fields.Find(se => se.Typ == FieldTyp.Year);
                if (f != null)
                {
                    prop = f.Column;
                }
            }

            // Date 
            if (typ == RelevantPropertyType.Date)
            {
                // 1. Prio = Generell die Frage nach einem Datum 
                IField? dateField = fields.Where(se => se.Typ == FieldTyp.Date).FirstOrDefault();
                if (dateField != null)
                {
                    prop = dateField.Column;
                }
                else
                {
                    // 2. Prio = Ende Termin 
                    IField? endField = fields.Where(se => se.Typ == FieldTyp.DateTime).FirstOrDefault();
                    if (endField != null)
                    {
                        prop = endField.Column;
                    }
                    else
                    {
                        // 3. Prio = Start Termin 
                        IField? startField = fields.Where(se => se.Typ == FieldTyp.Time).FirstOrDefault();
                        if (startField != null)
                        {
                            prop = startField.Column;
                        }
                    }
                }
            }

            // Property 
            return prop;
        }

        // To String 
        public override string ToString()
        {
            return ((string.IsNullOrEmpty(Title)) ? Name : Title);
        }
    }



    public interface IItemEventArgs
    {
        ISqlDatabaseService SqlService { get; set; }
        ISecurityService SecurityService { get; set; }
        IAppService AppService { get; set; }
        IDTO Item { get; set; }
        IDTO Related { get; set; }

    }
    public class ItemEventArgs : IItemEventArgs
    {
        public ISqlDatabaseService SqlService { get; set; }
        public ISecurityService SecurityService { get; set; }
        public IAppService AppService { get; set; }
        public IDTO Item { get; set; }
        public IDTO? Related { get; set; }

        public ItemEventArgs(IDTO main, IDTO? related,
                ISqlDatabaseService sql, ISecurityService auth, IAppService app)
        {
            SqlService = sql;
            SecurityService = auth;
            AppService = app;

            Item = main;
            Related = related;

            if (SqlService == null)
                throw new Exception("Sql Service");

            if (SecurityService == null)
                throw new Exception("Security Service");

            if (AppService == null)
                throw new Exception("App Service");

            if (Item == null)
                throw new Exception("Item");
        }
     
    }
}
