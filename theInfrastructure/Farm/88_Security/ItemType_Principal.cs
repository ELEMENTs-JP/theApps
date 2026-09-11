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
            if (ast == AssociationTyp.Children)
            { 
                ItemTypes.Add(new ItemType_User());
            }

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            List<IField> Fields = new();

            Fields.Add(new Field()
            {
                Title = "Aktiv",
                Typ = FieldTyp.CheckBox,
                Column = "IsActive",
                DefaultValue = "true",
                CSS = " col-12 col-md-6 col-lg-6 "
            });

            return Fields;
        }
    }
}
