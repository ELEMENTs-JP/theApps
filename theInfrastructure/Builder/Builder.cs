using System;
using System.Collections.Generic;
using System.Linq;

namespace theInfrastructure
{
    public class Builder
    {
        // Statisches HashSet, das exakt einmal beim Laden der Klasse im Speicher liegt
   



        private readonly ISqlDatabaseService _sqlService;

        public Builder(ISqlDatabaseService sql)
        {
            _sqlService = sql;
        }

        public ComponentDefinition? GetDefinition(string name)
        {
            return Helper.ControlDefinitions.FirstOrDefault(se => se.Name == name);
        }

        public List<IDynamicComponent> GetComponents()
        {
            List<IDynamicComponent> comps = new List<IDynamicComponent>(Helper.ControlDefinitions.Count);

            foreach (var def in Helper.ControlDefinitions)
            {
                comps.Add(BuildComponent(def));
            }

            return comps;
        }

        public IDynamicComponent BuildComponent(ComponentDefinition comp)
        {
            return new DynamicRazorComponent(comp);
        }

        public IDynamicComponent BuildComponent(string nameSpace, string className)
        {
            ComponentDefinition comp = new ComponentDefinition
            {
                Namespace = nameSpace,
                ClassName = className
            };
            return new DynamicRazorComponent(comp);
        }
    }
}