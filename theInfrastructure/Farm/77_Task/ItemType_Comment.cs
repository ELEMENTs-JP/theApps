using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Comment : BaseItemType, IItemType
    {
        public ItemType_Comment()
        {
            Name = "Comment";
            Title = "Kommentar";
            Typ = ItemTypeTyp.Comment;
            this.InSubNavigation = false;
            this.ShowInTaskBarNavigation = false;
            this.IsNavigation = false;
        }

        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Comment", Typ = FieldTyp.TextArea, Column = "Comment", CSS = " col-12 col-md-6 col-lg-12 ", OnDevice = DeviceDisplay.Desktop });

            return Fields;
        }

    }
}
