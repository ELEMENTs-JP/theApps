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
            InSubNavigation = false;
            Typ = ItemTypeTyp.User;
        }

   
        public override async Task<List<IItemType>> GetItemTypes(AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new List<IItemType>();

            if (ast == AssociationTyp.Related)
            {
                ItemTypes.Add(new ItemType_Favorite());
            }

            if (ast == AssociationTyp.Children)
            { 
                ItemTypes.Add(new ItemType_Permission());
            }
            if (ast == AssociationTyp.Default)
            {
                
            }
            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Nutzer", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "E-Mail", Typ = FieldTyp.Email, Column = "Mail", CSS = " col-12 col-md-6 col-lg-6 " });
            
            Fields.Add(new Field() { 
                Title = "Password", Typ = FieldTyp.Password, 
                Column = "Password", DefaultValue="Password",
                OnDisplay = new FieldDisplay(false, false, false, false),
                CSS = " col-12 col-md-6 col-lg-6 " });
            
            Fields.Add(new Field() 
            { 
                Title = "Administrator", Typ = FieldTyp.CheckBox, 
                Column = "IsAdmin", DefaultValue="false",
                CSS = " col-12 col-md-6 col-lg-6 " });
            Fields.Add(new Field() 
            { 
                Title = "Aktiv", Typ = FieldTyp.CheckBox, 
                Column = "IsActive", DefaultValue="true",
                CSS = " col-12 col-md-6 col-lg-6 " });
            
            
            Fields.Add(new Field() { Title = "Wallpaper", Typ = FieldTyp.Text, Column = "Wallpaper", CSS = " col-12 col-md-12 col-lg-12 " });

            // Fields.Add(new Field() { Title = "Anzeige", Typ = FieldTyp.Text, Column = "Anzeige", CSS = " col-6 ", Condition = new FieldCondition("Condition","Test") });
            // Fields.Add(new Field() { Title = "Condition", Typ = FieldTyp.Text, Column = "Condition", CSS = " col-6 " });

            return Fields;
        }
    }
}
