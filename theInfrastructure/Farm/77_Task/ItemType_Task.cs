using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Name = "Task";
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field()
            {
                Title = "Owner",
                Typ = FieldTyp.Select,
                ItemType = "User",
                Column = "Owner",
                CSS = " col-12 col-md-6 col-lg-6 ",
                OnDevice = DeviceDisplay.Desktop
            });

            Fields.Add(new Field() { Title = "Projekt", Typ = FieldTyp.Text, Column = "Projekt",
                CSS = " col-12 col-md-6 col-lg-6 ",
                IsNecessary =true, OnDevice = DeviceDisplay.Desktop });
            
            // Performance 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Performance));

            // Description 
            Fields.AddRange(Field.DefaultFields(DefaultFieldTypes.Description));

            return Fields;
        }

    }
}
