using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace theInfrastructure
{
    public class Builder
    {
        public List<ComponentDefinition> Definitions { get; set; } = new();
        ISqlDatabaseService sqlService;
        public Builder(ISqlDatabaseService sql) 
        {
            sqlService = sql;

            Init();
        }

        private async void Init()
        {
            Definitions = new();
            Definitions.Add(new ComponentDefinition() { Name = "Test UI", Namespace = "theInfrastructure", ClassName = "SimpleUICtl", CSS = " col-12 my-2" });
        }

        public List<IDynamicComponent> GetComponents()
        {
            List<IDynamicComponent> comps = new List<IDynamicComponent>();
            
            foreach (var def in Definitions)
            {
                comps.Add(BuildComponent(def));
            }

            return comps;
        }
        public IDynamicComponent BuildComponent(ComponentDefinition comp)
        {
            DynamicRazorComponent toBuildComponent = new DynamicRazorComponent(comp);
            return toBuildComponent;
        }
    }
}
