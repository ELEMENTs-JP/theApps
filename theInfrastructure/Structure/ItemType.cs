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
        bool AppItemType { get; set; }
        int Order { get; set; }

        // ItemType 
        Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association);

        // Events 
        Task<IDTO> OnCreateItem(IItemEventArgs args);
        Task<IDTO> AfterCreateItem(IItemEventArgs args);
        Task<IDTO> OnUpdateItem(IItemEventArgs args);
        Task<IDTO> OnDeleteItem(IItemEventArgs args);
        Task<IDTO> OnDragDropItem(IItemEventArgs args);

        Task<(IDTO, IDTO)> OnAssignItem(IItemEventArgs args);
        Task<(IDTO, IDTO)> OnRemoveItem(IItemEventArgs args);



        // Fields 
        Task<List<IField>> GetFields(string view = "");
        Task<string> GetRelevantPropertyName(RelevantPropertyType typ = RelevantPropertyType.Date);


        
    }
    public class  BaseItemType : IItemType
    {
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Icon Icon { get; set; } = Icon.NULL;
        public bool IsNavigation { get; set; } = true; // In Hauptnavigation anzeigen 
        public bool InSubNavigation { get; set; } = false; // In untergeordneter Navigation anzeigen 
        public bool ShowInTaskBarNavigation { get; set; } = true; // In der Taskbar unten rechts anzeigen 
        public bool AppItemType { get; set; } = false; // Definiert den HauptitemType der App für die Dashboard Selektion 
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


            if (ast == AssociationTyp.Parents)
            {

            }
            if (ast == AssociationTyp.Parallels)
            {

            }
            if (ast == AssociationTyp.Related)
            {



                if (this.Name != "File")
                {
                    ItemTypes.Add(new ItemType_File());
                }
                if (this.Name != "Audio")
                {
                    ItemTypes.Add(new ItemType_Audio());
                }
                if (this.Name != "Video")
                {
                    ItemTypes.Add(new ItemType_Video());
                }
                if (this.Name != "Image")
                {
                    ItemTypes.Add(new ItemType_Image());
                }

            }
            if (ast == AssociationTyp.Children)
            {
                if (this.Name != "Task")
                {
                    ItemTypes.Add(new ItemType_Task());
                }
                if (this.Name != "Checklist")
                {
                    ItemTypes.Add(new ItemType_Checklist());
                }
                if (this.Name != "Appointment")
                {
                    ItemTypes.Add(new ItemType_Appointment());
                }
                if (this.Name != "Link")
                {
                    ItemTypes.Add(new ItemType_Link());
                }

            }

            if (ast == AssociationTyp.Default)
            {
                if (this.Name != "Comment")
                { 
                    ItemTypes.Add(new ItemType_Comment());
                }
                if (this.Name != "Note")
                {
                    
                    ItemTypes.Add(new ItemType_Note());
                }
                if (this.Name != "News")
                {
                    ItemTypes.Add(new ItemType_News());
                }
            }



            return ItemTypes;
        }

        public virtual async Task<IDTO> OnCreateItem(IItemEventArgs args)
        {
            // Drag Drop Functionality 
            if (string.IsNullOrEmpty(args.Item["Zone"].ToSecureString()))
            { 
                args.Item["Zone"] = "Default";
                args.Item["Sort"] = "1";

                // Update 
                await args.SqlService.Update(args.Item);
            }

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

            // Datum 
            string date = await GetRelevantPropertyName(RelevantPropertyType.Date);
            if (!string.IsNullOrEmpty(date))
            {
                // Value 
                string val = args.Item[date].ToSecureString();

                // Empty 
                if (string.IsNullOrEmpty(val))
                {
                    // set Year 
                    args.Item[date] = DateTime.Now.Date.ToDateField();

                    // Update 
                    await args.SqlService.Update(args.Item);
                }
            }

            // Start 
            string start = await GetRelevantPropertyName(RelevantPropertyType.Start);
            if (!string.IsNullOrEmpty(start))
            {
                // Value 
                string val = args.Item[start].ToSecureString();

                // Empty 
                if (string.IsNullOrEmpty(val))
                {
                    // set Year 
                    args.Item[start] = DateTime.Now.RoundUpToNext15Minutes().ToShortTimeString();

                    // Update 
                    await args.SqlService.Update(args.Item);
                }
            }

            // Ende 
            string end = await GetRelevantPropertyName(RelevantPropertyType.End);
            if (!string.IsNullOrEmpty(end))
            {
                // Value 
                string val = args.Item[end].ToSecureString();

                // Empty 
                if (string.IsNullOrEmpty(val))
                {
                    // set Year 
                    args.Item[end] = DateTime.Now.AddHours(1).RoundUpToNext15Minutes().ToShortTimeString();

                    // Update 
                    await args.SqlService.Update(args.Item);
                }
            }

            // Stop
            string stop = await GetRelevantPropertyName(RelevantPropertyType.Stop);
            if (!string.IsNullOrEmpty(stop))
            {
                // Value 
                string val = args.Item[stop].ToSecureString();

                // Empty 
                if (string.IsNullOrEmpty(val))
                {
                    // set Year 
                    args.Item[stop] = DateTime.Now.AddDays(14).ToDateField();

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

            // Progress 
            if (typ == RelevantPropertyType.Progress)
            {
                // Progress 
                IField? f = fields.Find(se => se.Typ == FieldTyp.Progress);
                if (f != null)
                {
                    prop = f.Column;
                }
            }

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
                IField? dateField = fields.Find(se => se.Typ == FieldTyp.Date 
                        && (se.Column == "Date" || se.Column == "Datum" || se.Column == "DueTo"));
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

            // Start 
            if(typ == RelevantPropertyType.Start)
            {
                IField? startField = fields.Find(se => se.Typ == FieldTyp.Time && se.Column == "Start" && se.Typ == FieldTyp.Time);
                if (startField != null)
                {
                    prop = startField.Column;
                }
            }

            // End 
            if(typ == RelevantPropertyType.End)
            {
                IField? endField = fields.Find(se => se.Typ == FieldTyp.Time && (se.Column == "End" || se.Column == "Ende" && se.Typ == FieldTyp.Time));
                if (endField != null)
                {
                    prop = endField.Column;
                }
            }

            // Stop 
            if (typ == RelevantPropertyType.Stop)
            {
                IField? endField = fields.Find(se => se.Typ == FieldTyp.Time && (se.Column == "Stop" && se.Typ == FieldTyp.Date));
                if (endField != null)
                {
                    prop = endField.Column;
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
