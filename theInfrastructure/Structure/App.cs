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
        List<IItemType> ItemTypes { get; set; }

        Task Init();
    }
    public class BaseApp : IApp
    {
        public BaseApp()
        {

        }
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public List<IItemType> ItemTypes { get; set; } = new();

        public virtual async Task Init()
        {
            // ItemTypes 


        }
    }

}
