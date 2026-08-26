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
