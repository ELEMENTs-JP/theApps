using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_App : BaseItemType, IItemType
    {
        public ItemType_App()
        {
            Name = "App";
        }

        public override async Task<List<IItemType>> GetItemTypes(
            AssociationTyp ast = AssociationTyp.Association)
        {
            List<IItemType> ItemTypes = new();

            if (ast == AssociationTyp.Children)
            { 
                // ItemTypes 
                ItemTypes.Add(new ItemType_ItemType());
                ItemTypes.Add(new ItemType_Page());
                ItemTypes.Add(new ItemType_Board());
            }

            return ItemTypes;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            List<IField> Fields = new();

            // Fields 
            Fields = new List<IField>();
            
            Fields.Add(new Field() { Title = "Group", Description="Legt die Gruppe der Navigation fest.", 
                Typ = FieldTyp.DropDown, Column = "Group", CSS = " col-8 ", 
                Items = new() {  "Strategy", "Tactic", "Operation", "Cross", "Support" } });
            
            
            Fields.Add(new Field() { Title = "Aktiv", Description="Legt fest ob die App aktiv nutzbar ist.", 
                Typ = FieldTyp.CheckBox, Column = "IsActive", CSS = " col-4 " });
            
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Beschreibung", Typ = FieldTyp.TextArea, Column = "Description", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }
    }
}
