using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
  

    public class DragDropElement
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Typ { get; set; } = string.Empty; // definiert den Typ (z.B. 57 oder 75 oder 444
        public int Sort { get; set; } = 1; // Reihenfolge 
        public string Zone { get; set; } = string.Empty; // Zone im Layout 
        public ComponentDefinition Control { get; set; } = new(); // für Dynamic Control 
        public List<Column> Columns { get; set; } = new List<Column>(); // für Layout Rows in der Page 
        public Icon Icon { get; set; } = Icon.NULL;
    }
    public class Column
    {
        public int Width { get; set; } = 12;
        public string Zone { get; set; } = string.Empty;
    }
}
