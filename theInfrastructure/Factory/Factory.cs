using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace theInfrastructure
{
    public class Factory
    {
        ISqlDatabaseService sqlService;
        public Factory(ISqlDatabaseService sql) 
        {
            sqlService = sql;
        }

        // App 
        public IApp BuildApp(string name)
        {
            string className = $"App_{name}";

            Type? type = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name == className &&
                                                     typeof(IApp).IsAssignableFrom(t));

            if (type == null)
                throw new ArgumentException($"Keine App für '{name}' gefunden.");

            return (IApp)Activator.CreateInstance(type)!;
        }

        // ItemType 
        public IItemType BuildItemType(string name)
        {
            string className = $"ItemType_{name}";

            Type? type = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name == className &&
                                                     typeof(IItemType).IsAssignableFrom(t));

            if (type == null)
                throw new ArgumentException($"Kein ItemType für '{name}' gefunden.");

            return (IItemType)Activator.CreateInstance(type)!;
        }
        public List<IItemType> InjectItemTypes(IApp app)
        {
            List<IItemType> itemTypes = new();

            if (app.GetType() == typeof(App_Template))
            {

            }

            return itemTypes;
        }

        // Fields 
        public IField BuildField(FieldTyp fieldTyp)
        {
            foreach (FieldTyp field in Enum.GetValues(typeof(FieldTyp)))
            {
                if (field == fieldTyp)
                {
                    IField f = new Field();
                    f.Title = field.ToString();
                    f.Typ = field;
                    f.Column = field.ToString();
                    f.CSS = "col";
                    return f;
                }
            }

            return null;
        }
        public List<IField> InjectFields(IItemType itemType)
        {
            List<IField> fields = new();

            if (itemType.GetType() == typeof(ItemType_Template))
            {

            }

            return fields;
        }
    }
}
