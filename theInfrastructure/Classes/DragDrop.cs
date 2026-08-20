using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class ControlTyp
    {
        public string Name { get; set; } = "the Control";
    }

    public class DragDropElement
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public int Sort { get; set; } = 1;
        public string Typ { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public ControlTyp Control { get; set; } = new();
        public List<Column> Columns { get; set; } = new List<Column>();
    }
    public class Column
    {
        public int Width { get; set; } = 12;
        public string Zone { get; set; } = Guid.NewGuid().ToString();
    }
}
