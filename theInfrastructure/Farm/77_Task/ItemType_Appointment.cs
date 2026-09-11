using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Appointment : BaseItemType, IItemType
    {
        public ItemType_Appointment()
        {
            Name = "Appointment";
            Title = "Termin";
            Typ = ItemTypeTyp.Appointment;
            this.Order = 2;
        }

        // ItemTypes 
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();


            if (ast == AssociationTyp.Related)
            {
                ItemTypes.Add(new ItemType_Task());
            }
            if (ast == AssociationTyp.Children)
            {
                ItemTypes.Add(new ItemType_Checklist());
            }

            if (ast == AssociationTyp.Default)
            {
                ItemTypes.Add(new ItemType_Comment());
                ItemTypes.Add(new ItemType_File());
            }

            return ItemTypes;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Appointment));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));
            
            return Fields;
        }

    }
}
