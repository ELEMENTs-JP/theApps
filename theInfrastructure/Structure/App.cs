using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public interface IApp
    {
        List<IItemType> ItemTypes { get; set; }
    }
    public class BaseApp : IApp
    {
        public List<IItemType> ItemTypes { get; set; } = new();
    }

}
