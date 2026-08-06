using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    // Interface 
    public interface IItemType
    {
        List<IField> Fields { get; set; }
        void Init();
    }
    public class  BaseItemType : IItemType
    {
        public BaseItemType()
        { 
        
        }
        public List<IField> Fields { get; set; } = new();

        public virtual void Init()
        { 
        
        }
    }
}
