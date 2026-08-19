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
        ItemTypeTyp Typ { get; set; }
        bool InSubNavigation { get; set; }

        public Task<List<IItemType>> GetItemTypes();
        public Task<List<IField>> GetFields();
        public Task<string> GetRelevantPropertyName(RelevantPropertyType typ = RelevantPropertyType.Date);
    }
    public class  BaseItemType : IItemType
    {
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool InSubNavigation { get; set; } = true;
        public ItemTypeTyp Typ { get; set; } = ItemTypeTyp.Item;
        public BaseItemType()
        { 
        
        }

        public virtual async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }
        public virtual async Task<List<IField>> GetFields()
        {
            List<IField> Fields = new();

            return Fields;
        }
        public override string ToString()
        {
            return ((string.IsNullOrEmpty(Title)) ? Name : Title);
        }

        public async Task<string> GetRelevantPropertyName(RelevantPropertyType typ = RelevantPropertyType.Date)
        {

            string propertyName = string.Empty;

            List<IField> fields = await this.GetFields(); 

            // Date 
            if (typ == RelevantPropertyType.Date)
            {
                // 1. Prio = Generell die Frage nach einem Datum 
                IField dateField = fields.Where(se => se.Typ == FieldTyp.Date).FirstOrDefault();
                if (dateField != null)
                {
                    propertyName = dateField.Column;
                }
                else
                {
                    // 2. Prio = Ende Termin 
                    IField endField = fields.Where(se => se.Typ == FieldTyp.DateTime).FirstOrDefault();
                    if (endField != null)
                    {
                        propertyName = endField.Column;
                    }
                    else
                    {
                        // 3. Prio = Start Termin 
                        IField startField = fields.Where(se => se.Typ == FieldTyp.Time).FirstOrDefault();
                        if (startField != null)
                        {
                            propertyName = startField.Column;
                        }
                    }
                }
            }

            // Property
            return propertyName;
        }


    }
}
