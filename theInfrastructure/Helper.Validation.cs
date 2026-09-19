using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        private static readonly HashSet<string> PersonalizedItemTypes = new(StringComparer.Ordinal)
        {
            "Task", "Note", "Appointment", "Reminder"
        };


        public static bool IsPersonalizedItemType(this IItemType itemType)
        {
            if (string.IsNullOrEmpty(itemType.Name))
            {
                return false;
            }

            return PersonalizedItemTypes.Contains(itemType.Name);
        }

    }
}
