using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public interface IApp
    {
        string Name { get; set; }
        string Group { get; set; } // Gruppierung in der Navigation // Allgemeine Typisierung
        Task<List<IItemType>> GetItemTypes();

    }
    public class BaseApp : IApp
    {
        public BaseApp()
        {

        }
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;

        public virtual async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }
    }

}
