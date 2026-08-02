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
    }

    public class Field : IField
    {
        public FieldTyp Typ { get; set; } = FieldTyp.Text;
        public string Title { get; set; } = string.Empty;
        public string Column { get; set; } = string.Empty;
    }
}
