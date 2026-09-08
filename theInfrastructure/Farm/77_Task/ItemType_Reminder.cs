using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Reminder : BaseItemType, IItemType
    {
        public ItemType_Reminder()
        {
            Name = "Reminder";
            Title = "Erinnerung";
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
