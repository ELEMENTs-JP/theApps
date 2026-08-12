using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theInfrastructure
{
    public class SystemConfiguration
    {
        public string Title { get; set; } = string.Empty;
        public string Sprache { get; set; } = string.Empty;
        public bool Akzente { get; set; } = false; // weiße Akzente anzeigen oder nicht 
    }
}
