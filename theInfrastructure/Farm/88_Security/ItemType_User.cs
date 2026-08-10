using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_User : BaseItemType, IItemType
    {
        public ItemType_User()
        {
            Name = "User";
            Init();
        }
        public List<IField> Fields { get; set; } = new();

        public override async Task Init()
        {
            await base.Init(); 

            // Fields 
            Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Nutzer", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "E-Mail", Typ = FieldTyp.Email, Column = "Mail", CSS = " col-12 col-md-6 col-lg-4 " });
            Fields.Add(new Field() { Title = "Password", Typ = FieldTyp.Password, Column = "Password", CSS = " col-12 col-md-6 col-lg-4 " });
        }
    }
}
