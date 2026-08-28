using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Principal : BaseItemType, IItemType
    {
        public ItemType_Principal()
        {
            Name = "Principal";
        }
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new List<IItemType>();

            // ItemTypes 
            ItemTypes.Add(new ItemType_User());

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            List<IField> Fields = new();
            return Fields;
        }
    }
}
