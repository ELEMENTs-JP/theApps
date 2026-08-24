using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        // Navigation 
        public static void NavToApp(this NavigationManager nm, string App)
        {
            nm.NavigateTo("/App/" + App, false);
        }
        public static void NavToItem(this NavigationManager nm, string ItemType, Guid GUID)
        {
            nm.NavigateTo("/Item/" + ItemType + "/" + GUID.ToString(), false);
        }
        public static void NavToLibrary(this NavigationManager nm, string ItemType, ItemTypeTyp typ = ItemTypeTyp.Item)
        {
            nm.NavigateTo(LibraryUrl(ItemType, typ), false);
        }
        public static string LibraryUrl(string ItemType, ItemTypeTyp typ = ItemTypeTyp.Item)
        {
            if (typ == ItemTypeTyp.Item)
            {
                return "/Items/" + ItemType;
            }
            if (typ == ItemTypeTyp.File)
            {
                return "/File/" + ItemType;
            }
            if (typ == ItemTypeTyp.Appointment)
            {
                return "/Calendar/" + ItemType;
            }
            if (typ == ItemTypeTyp.Hierarchy)
            {
                return "/Hierarchy/" + ItemType;
            }

            return "/Items/" + ItemType;
        }
    }
}
