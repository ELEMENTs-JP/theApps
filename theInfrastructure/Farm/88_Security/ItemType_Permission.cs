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
            string typ = args.Item["Typ"].ToSecureString();
            if (typ == "App")
            {
                perm =
                args.Item["Typ"].ToSecureString() + "-" +
                args.Item["App"].ToSecureString() + "-" +
                "0" + "-" +
                "0" + "-" +
                args.Item["AllowDeny"].ToSecureString();
            }
            if (typ == "ItemType")
            {
                perm =
                args.Item["Typ"].ToSecureString() + "-" +
                "0" + "-" +
                args.Item["ItemType"].ToSecureString() + "-" +
                args.Item["Function"].ToSecureString() + "-" +
                args.Item["AllowDeny"].ToSecureString();
            }
            //if (typ == "Function")
            //{
            //    perm =
            //    args.Item["Typ"].ToSecureString() + "-" +
            //    "0" + "-" +
            //    "0" + "-" +
            //    args.Item["Function"].ToSecureString() + "-" +
            //    args.Item["AllowDeny"].ToSecureString();
            //}
            

            args.Item["Permission"] = perm;

            // Refresh current User 
            await args.SecurityService.RefreshUser();

            // return 
            return args.Item;
        }
        public override async Task<IDTO> OnDeleteItem(IItemEventArgs args)
        {
            // Refresh current User 
            await args.SecurityService.RefreshUser();

            // return 
            return args.Item;
        }
        public override async Task<List<IField>> GetFields(string view = "")
        {
            // Fields 
            List<IField> Fields = new List<IField>();

            Fields.Add(new Field() { Title = "Typ", Typ = FieldTyp.HR });

            Fields.Add(new Field() { Title = "Typ", Typ = FieldTyp.DropDown, 
                Items = new() { "App", "ItemType" },
                Column = "Typ", CSS = " col-12 col-md-4 col-lg-4 " });

            Fields.Add(new Field() { Title = "Auswahl", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "App", Typ = FieldTyp.AppList, 
                Condition = new FieldCondition("Typ", "App", "!=", "Hide"),
                Column = "App", CSS = " col-12 " });
            
            Fields.Add(new Field() { Title = "ItemType", Typ = FieldTyp.ItemTypeList,
                Condition = new FieldCondition("Typ", "ItemType", "!=", "Hide"),
                Column = "ItemType", CSS = " col-6 " });

            Fields.Add(new Field()
            {
                Title = "Function",
                Typ = FieldTyp.FunctionList,
                Condition = new FieldCondition("Typ", "ItemType", "!=", "Hide"),
                Column = "Function",
                CSS = " col-6 "
            });

            Fields.Add(new Field() { Title = "Berechtigung", Typ = FieldTyp.HR });
            Fields.Add(new Field() { Title = "Permission", Typ = FieldTyp.TextBlock, 
                Column = "Permission", CSS = " col-12 col-md-8 col-lg-8 " });
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
