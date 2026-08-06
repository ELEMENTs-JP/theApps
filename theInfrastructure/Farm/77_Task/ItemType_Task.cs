using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Task : BaseItemType, IItemType
    {
        public ItemType_Task()
        {
            Init();
        }
        public List<IField> Fields { get; set; } = new();

        public override void Init()
        {
            base.Init(); 

            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Title", Typ = FieldTyp.Text, Column = "Title" });
            Fields.Add(new Field() { Title = "Status", Typ = FieldTyp.Text, Column = "Status" });
            Fields.Add(new Field() { Title = "Description", Typ = FieldTyp.TextArea, Column = "Description" });
        }
    }
}
