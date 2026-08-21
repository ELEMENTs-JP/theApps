using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theInfrastructure
{
    public class LocaleConfiguration
    {
        public string Sprache { get; set; } = string.Empty; // definiert die Sprache der UI 
        public string Currency { get; set; } = string.Empty; // legt die genutzte Währung im System fest 

        public void Save()
        {
            LocaleConfiguration config = this as LocaleConfiguration;

            Serializer.Save<LocaleConfiguration>(config, "locale.config");
        }
        public static LocaleConfiguration Load()
        {
            return Serializer.Load<LocaleConfiguration>("locale.config");
        }
    }
    public class SystemConfiguration
    {
        public string Title { get; set; } = string.Empty;
        public string Background { get; set; } = string.Empty;
        public bool Akzente { get; set; } = false; // weiße Akzente anzeigen oder nicht 
        public bool Trash { get; set; } = false; // zeigt den Papierkorb an oder nicht 
        public bool Archive { get; set; } = false; // zeigt das Archiv an 
        public bool TaskBar { get; set; } = true; // zeigt die Taskbar unten rechts an
        public bool Clock { get; set; } = true; // zeigt die Uhr an 
        public bool AllowFileUploads { get; set; } = true;
        public int MaxFileSizeInMB { get; set; } = 20;
        public void Save()
        {
            SystemConfiguration config = this as SystemConfiguration;

            Serializer.Save<SystemConfiguration>(config, "system.config");
        }
        public static SystemConfiguration Load()
        {
            return Serializer.Load<SystemConfiguration>("system.config");
        }

    }
}
