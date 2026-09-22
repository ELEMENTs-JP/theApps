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
        public async Task<IApp?> BuildApp(string name)
        {
            string className = $"App_{name}";

            Type? type = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name == className &&
                                                     typeof(IApp).IsAssignableFrom(t));

            if (type == null)
            {
                IQueryParameter qp = new QueryParameter();
                qp.ItemType = "App";
                qp.MasterGUID = sqlService.MasterGUID;
                IQueryResult result = await sqlService.GetItems(qp);

                IDTO dto = result.Items.FirstOrDefault(se => se.Title == name);
                if (dto != null)
                {
                    IApp template = new App_Template(dto, sqlService);
                    return template;
                }
            }

            if (type == null)
            {
                return null;
            }

            return (IApp)Activator.CreateInstance(type)!;
        }

        // ItemType 
        public async Task<IItemType?> BuildItemType(string name)
        {
            string className = $"ItemType_{name}";

            Type? type = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name == className &&
                                                     typeof(IItemType).IsAssignableFrom(t));

            if (type == null)
            {
                IQueryParameter qp = new QueryParameter();
                qp.ItemType = "ItemType";
                qp.MasterGUID = sqlService.MasterGUID;
                IQueryResult result = await sqlService.GetItems(qp);
                
                IDTO? dto = result.Items.FirstOrDefault(se => se.Title == name);
                if (dto != null)
                { 
                    IItemType template = new ItemType_Template(dto, sqlService);
                    return template;
                }
            }

            if (type == null)
            {
                return null;
            }

            return (IItemType)Activator.CreateInstance(type)!;
        }


        // Fields 
        public IField BuildField(FieldTyp fieldTyp)
        {
            string typName = fieldTyp.ToString();

            return new Field
            {
                Title = typName,
                Typ = fieldTyp,
                Column = typName,
                CSS = "col"
            };
        }
        public IField BuildField(string fieldTyp)
        {
            if (Enum.TryParse<FieldTyp>(fieldTyp, out var field))
            {
                return new Field
                {
                    Title = fieldTyp,
                    Typ = field,
                    Column = fieldTyp,
                    CSS = "col"
                };
            }

            return null;
        }

    }
}
