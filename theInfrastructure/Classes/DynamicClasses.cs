using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // Interface 
    public interface IDynamicComponent
    {
        Type ComponentType { get; set; }
        IDictionary<string, object> Parameters { get; set; }
        ComponentConfiguration Configuration { get; set; }
        string CssColumnClass { get; set; }
        bool IsActive { get; set; }
        public IItemType GetItemType();

        IList<IDynamicComponent> Children { get; set; }
    }
    
    // Base Class 
    public class BaseDynamicComponent : IDynamicComponent
    {
        public Type ComponentType { get; set; } = null;
        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public ComponentConfiguration Configuration { get; set; } = new ComponentConfiguration();
        public string CssColumnClass { get; set; } = "col";
        public bool IsActive { get; set; } = true;
        public IItemType GetItemType()
        {
            return null;
        }
        public IList<IDynamicComponent> Children { get; set; } = new List<IDynamicComponent>();
    }
    
    // Template 
    public class DynamicRazorComponent : BaseDynamicComponent, IDynamicComponent
    {
        public DynamicRazorComponent(string theNamespace = "tpt.Controls.v2",
                string theClassName = "FileInputBox", string css = "col-12 my-2",
                Dictionary<string, object> items = null)
        {
            // Type 
            this.ComponentType = Helper.SpecificType(theNamespace, theClassName);

            // Style
            this.CssColumnClass = css;

            // Parameter
            // Initialisierung, falls null übergeben wurde
            if (items != null)
            {
                this.Parameters = items;
            }
        }
    }
    
    // Configuration 
    public class ComponentConfiguration
    {
        public string ItemType { get; set; }
        public string Title { get; set; }
        IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public ComponentConfiguration()
        {

        }

        public void Add(string key, object value)
        {
            try
            {
                Parameters.Add(key, value);
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim hinzufügen.");
            }
        }

        public object GetParameterByKey(string key)
        {
            try
            {
                KeyValuePair<string, object> result = Parameters.Where(se => se.Key == key).FirstOrDefault();
                return result.Value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
