using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    // Interface 
    public interface IItemType
    {
        string Name { get; set; }
        List<IItemType> ItemTypes { get; set; }
        List<IField> Fields { get; set; }
        void Init();
    }
    public class  BaseItemType : IItemType
    {
        public string Name { get; set; } = string.Empty;
        public BaseItemType()
        { 
        
        }


        public List<IItemType> ItemTypes { get; set; } = new();
        public List<IField> Fields { get; set; } = new();


        public virtual void Init()
        { 
        
        }
    }
}
