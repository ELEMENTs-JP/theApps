using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    public interface IField
    {
        string Column { get; set; }
        FieldTyp Typ { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Placeholder { get; set; }
        string CSS { get; set; }

        DeviceDisplay OnDevice { get; set; }
    }

    public class Field : IField
    {
        public Field()
        { }
        public FieldTyp Typ { get; set; } = FieldTyp.Text;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Placeholder { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
        public string CSS { get; set; } = "col";
        public DeviceDisplay OnDevice { get; set; } = DeviceDisplay.NULL;
    }
}
