using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theInfrastructure
{
    public class DTO : IDTO
    {
        public Guid GUID { get; set; } = Guid.Empty;
        public Guid MasterGUID { get; set; } = Guid.Empty;
        public string? ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? Matchcode { get; set; } = string.Empty;
        // not mapped 
        public string? RelationType { get; set; } = string.Empty;

        // Properties 
        public List<ItemProperty> Properties { get; set; } = new List<ItemProperty>();

        [NotMapped]
        public string this[string propertyName]
        {
            get
            {
                string searchPattern = propertyName ?? string.Empty;
                ItemProperty prop = Properties.Find(p => p?.Property != null && p.Property.Contains(searchPattern, StringComparison.OrdinalIgnoreCase)) ?? ItemProperty.Empty(searchPattern);
                return prop.Value;
            }
            set
            {
                // Wert setzen 
                var existing = Properties.Find(p => p.Property.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    existing.Value = value;
                }
                else
                {
                    Properties.Add(new ItemProperty { Property = propertyName, Value = value });
                }
            }
        }
    }
}
