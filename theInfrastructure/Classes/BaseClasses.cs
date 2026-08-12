using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace theInfrastructure
{
    public class DTO : ISE
    {
        public string? ID { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
    }
}
