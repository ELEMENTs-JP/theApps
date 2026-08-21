using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class ComponentDefinition
    {
        public string Name { get; set; } = "the Control";
        public string Namespace { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string CSS { get; set; } = "col-12 my-2";
        public Dictionary<string, object> ParameterList = null;
        public IItemType ItemType { get; set; } = null;
    }

    // Interface 
    public interface IDynamicComponent
    {
        Type ComponentType { get; set; }
        IDictionary<string, object> Parameters { get; set; }
        string CssColumnClass { get; set; }
        bool IsActive { get; set; }
        IItemType ItemType { get; set; }

        IList<IDynamicComponent> Children { get; set; }
    }
    
    // Base Class 
    public class BaseDynamicComponent : IDynamicComponent
    {
        public Type ComponentType { get; set; } = null;
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public string CssColumnClass { get; set; } = "col";
        public bool IsActive { get; set; } = true;
        public IItemType ItemType { get; set; }
        public IList<IDynamicComponent> Children { get; set; } = new List<IDynamicComponent>();
    }
    
    // Template 
    public class DynamicRazorComponent : BaseDynamicComponent, IDynamicComponent
    {
        public DynamicRazorComponent(ComponentDefinition def)
        {
            // Check 
            if (string.IsNullOrEmpty(def.Namespace) || string.IsNullOrEmpty(def.ClassName))
                return;

            // Type 
            this.ComponentType = Helper.SpecificType(def.Namespace, def.ClassName);
            
            // ItemType 
            this.ItemType = def.ItemType;
            
            // Style
            this.CssColumnClass = def.CSS;

            // Parameter 
            if (def.ParameterList != null)
            {
                this.Parameters = def.ParameterList;
            }
        }
    }

}
