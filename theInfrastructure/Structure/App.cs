using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public interface IApp
    {
        string Title { get; set; }
        string Name { get; set; }
        string Group { get; set; } // Gruppierung in der Navigation // Allgemeine Typisierung
        bool IsNavigation { get; set; } // wird im App Selector // Navigation angezeigt oder nicht 
        bool IsActive { get; set; } // legt fest ob die App aktiv ist und verwendet werden kann (Feature on off) 
        Task<List<IItemType>> GetItemTypes();

    }
    public class BaseApp : IApp
    {
        public BaseApp()
        {

        }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public bool IsNavigation { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public virtual async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }

        public override string ToString()
        {
            return ((string.IsNullOrEmpty(Title)) ? Name : Title);
        }
    }

}
