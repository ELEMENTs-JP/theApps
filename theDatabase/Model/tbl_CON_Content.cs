using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using theInfrastructure;

namespace theDatabase
{
    public partial class tbl_CON_Content : IDTO, ISE
    {
        // Fields 
        #region Properties
        // Identify 
        [Key]
        [Required]
        public Guid GUID { get; set; }

        [Required]
        public Guid MasterGUID { get; set; }
        
        // Identify 
        public string? ID { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string ItemType { get; set; } = string.Empty;

        // Title // Content // Matchcode 
        [MaxLength(500)]
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; } = string.Empty;

        [MaxLength(4000)]
        public string? Matchcode { get; set; } = string.Empty;
        #endregion

        // Metadata 
        public Metadata Metadata { get; set; } = new();

        // Properties 
        public List<ItemProperty> Properties { get; set; } = new List<ItemProperty>();


        [NotMapped]
        public string this[string propertyName]
        {
            get
            {
                if (string.IsNullOrEmpty(propertyName))
                    return string.Empty;

                // 1. Suche nach existierendem Property-Eintrag
                ItemProperty prop = Properties.Find(p =>
                    p?.Property != null &&
                    p.Property.Contains(propertyName, StringComparison.OrdinalIgnoreCase));

                // 2. Wenn Wert vorhanden und nicht leer/null ist, diesen zurückgeben
                if (prop != null && !string.IsNullOrEmpty(prop.Value))
                {
                    return prop.Value;
                }

                // 3. Fallback auf den vordefinierten Default-Wert (O(1) Nachschlagezeit)
                if (Helper.GetPropertyDefaultValues().TryGetValue(propertyName, out string defaultValue))
                {
                    return defaultValue;
                }

                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(propertyName))
                    return;

                var existing = Properties.Find(p =>
                    p.Property != null &&
                    p.Property.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

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

        //[NotMapped]
        //public string this[string propertyName]
        //{
        //    get 
        //    {
        //        string searchPattern = propertyName ?? string.Empty;
        //        ItemProperty prop = Properties.Find(p => p?.Property != null && p.Property.Contains(searchPattern, StringComparison.OrdinalIgnoreCase)) ?? ItemProperty.Empty(searchPattern);
        //        return prop.Value;
        //    }
        //    set
        //    {
        //        // Wert setzen 
        //        var existing = Properties.Find(p => p.Property.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        //        if (existing != null)
        //        {
        //            existing.Value = value;
        //        }
        //        else
        //        {
        //            Properties.Add(new ItemProperty { Property = propertyName, Value = value });
        //        }
        //    }
        //}

        [NotMapped]
        public string? RelationType { get; set; } = string.Empty; // IRelationDTO 


    }
}
