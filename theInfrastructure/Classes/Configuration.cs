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
    public class LayoutConfiguration
    {
        public string Background { get; set; } = string.Empty;
        public bool Akzente { get; set; } = false; // weiße Akzente anzeigen oder nicht 
        public bool Blur { get; set; } = false; // Legt fest ob der Hintergrund blur ist 
        public bool Glass { get; set; } = false; // Legt fest ob der Vordergrund blur ist 

        public void Save()
        {
            LayoutConfiguration config = this as LayoutConfiguration;

            Serializer.Save<LayoutConfiguration>(config, "layout.config");
        }
        public static LayoutConfiguration Load()
        {
            return Serializer.Load<LayoutConfiguration>("layout.config");
        }
    }
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

        public bool Trash { get; set; } = false; // zeigt den Papierkorb an oder nicht 
        public bool Archive { get; set; } = false; // zeigt das Archiv an 
        public bool TaskBar { get; set; } = true; // zeigt die Taskbar unten rechts an
        public bool Clock { get; set; } = true; // zeigt die Uhr an 
        public bool EventHandler { get; set; } = true; // legt fest ob die Item (ItemType) Event Handler aktiviert sind oder nicht 
        public bool GlobalSearch { get; set; } = true; // legt fest ob die übergeordnete Suche angezeigt wird 
        public bool PDFView { get; set; } = true; // aktiviert / deaktiviert die Anzeige des PDF Viewer auf der Detail Page 
        public bool Hilfe { get; set; } = true; // aktiviert / deaktiviert die Anzeige der Hilfe auf der Detail Page 
        
        // Dateien 
        public bool AllowFileUploads { get; set; } = true;
        public int MaxFileSizeInMB { get; set; } = 20;
        public int SmallImagePixel { get; set; } = 300; // legt die Größe von verkleinerten Bildern für eine performante Preview fest 

        // Sicherheit 
        public bool FocusOnLogin { get; set; } = false;
        public string GueltigkeitAnmeldungDauer { get; set; } = "4";
        public int PasswordSize { get; set; } = 8;
        public bool AllowDauerhafteAnmeldung { get; set; } = true;
        public bool FocusOnRegister { get; set; } = false;
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
