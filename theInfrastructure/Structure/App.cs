using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public interface IApp
    {
        string Name { get; set; }
        List<IItemType> ItemTypes { get; set; }
    }
    public class BaseApp : IApp
    {
        public string Name { get; set; } = string.Empty;
        public List<IItemType> ItemTypes { get; set; } = new();
    }

}
