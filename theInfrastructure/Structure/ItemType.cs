using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    // Interface 
    public interface IItemType
    {
        List<IField> Fields { get; set; }
    }
    public class  ItemType : IItemType
    {
        public List<IField> Fields { get; set; } = new();
    }
}
