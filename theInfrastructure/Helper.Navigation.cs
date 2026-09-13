using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        public static IEnumerable<string> PrepareUriSegments(this IEnumerable<string>? segments)
        {
            if (segments == null)
            {
                return Enumerable.Empty<string>();
            }

            return segments.Where(segment => !IsNumber(segment));
        }

        private static bool IsNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // 2. Prüfung auf Ganzzahl (Integer / Long)
            if (long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
            {
                return true;
            }

            // 3. Prüfung auf Dezimalzahl (InvariantCulture & CurrentCulture für "." und ",")
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out _) ||
                decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out _))
            {
                return true;
            }

            return false;
        }
        public static List<IDTO> ValidateNavigateableItems(this List<IDTO> items)
        {
            List<string> notNavigateableItemTypes = new List<string>() { "Comment", "Principal", "User", "Permission" };
            return items.Where(item => !notNavigateableItemTypes.Contains(item.ItemType)).ToList();
        }
        public static bool ValidateVisibility(SecuredFeature feature, IItemType ItemType, IDTO User)
        {
            if (feature == SecuredFeature.NULL || ItemType == null)
                return false;

            // Kommentare grundsätzlich NEIN, keine Liste, kein Item 
            // RelatedItems == ja 
            if (ItemType.Typ == ItemTypeTyp.Comment)
            {
                return false;
            }

            // Sicherheitsrelevante Daten 
            if (ItemType.Name == "Principal" || ItemType.Name == "User" || ItemType.Name == "Permission")
            {
                return User["IsAdmin"].ToSecureBool();
            }


            return true;
        }

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
