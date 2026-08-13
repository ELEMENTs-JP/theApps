using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    // Interface 
    public interface IItemType
    {
        string Name { get; set; }
        string Description { get; set; }

        public Task<List<IItemType>> GetItemTypes();
        public Task<List<IField>> GetFields();
    }
    public class  BaseItemType : IItemType
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BaseItemType()
        { 
        
        }

        public virtual async Task<List<IItemType>> GetItemTypes()
        {
            List<IItemType> ItemTypes = new();

            return ItemTypes;
        }
        public virtual async Task<List<IField>> GetFields()
        {
            List<IField> Fields = new();

            return Fields;
        }

       
    }
}
