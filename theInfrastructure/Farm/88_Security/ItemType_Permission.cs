using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class  ItemType_Permission : BaseItemType, IItemType
    {
        public ItemType_Permission()
        {
            Name = "Permission";
            InSubNavigation = false;
        }

        // ItemTypes 
        public override async Task<IDTO> OnUpdateItem(IItemEventArgs args)
        {
            await base.OnUpdateItem(args);

            // Permission 
            string perm = string.Empty;

            perm = 
                args.Item["App"].ToSecureString() + "-" + 
                args.Item["ItemType"].ToSecureString() + "-" + 
                args.Item["Function"].ToSecureString() + "-" + 
                args.Item["AllowDeny"].ToSecureString();

            args.Item["Permission"] = perm;

            // return 
            return args.Item;
        }

        public override async Task<List<IField>> GetFields()
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Objekt", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "App", Typ = FieldTyp.AppList, Column = "App", CSS = " col-12 col-md-4 col-lg-4 " });
            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.ItemTypeList, Column = "ItemType", CSS = " col-12 col-md-4 col-lg-4 " });
            Fields.Add(new Field() { Title = "Function", Typ = FieldTyp.FunctionList, Column = "Function", CSS = " col-12 col-md-4 col-lg-4 " });

            Fields.Add(new Field() { Title = "Berechtigung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Permission", Typ = FieldTyp.TextBlock, Column = "Permission", CSS = " col-12 col-md-8 col-lg-8 " });
            Fields.Add(new Field() { 
                Title = "Allow / Deny", 
                Typ = FieldTyp.CheckBox, 
                Column = "AllowDeny", 
                TrueText = "Allow",
                FalseText = "Deny",
                CSS = " col-12 col-md-4 col-lg-4 " });

            return Fields;
        }
    }
}
