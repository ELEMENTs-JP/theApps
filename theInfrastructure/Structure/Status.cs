using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class Status
    {
        public static List<IDTO> DefaultStatus()
        {
            List<IDTO> Items = new();

            Items.Add(new DTO() { ID = "9", Title = "abgeschlossen" });
            Items.Add(new DTO() { ID = "7", Title = "zurückgestellt" });
            Items.Add(new DTO() { ID = "5", Title = "in Arbeit" });
            Items.Add(new DTO() { ID = "3", Title = "in Vorbereitung" });
            Items.Add(new DTO() { ID = "1", Title = "in Planung" });
            Items.Add(new DTO() { ID = "0", Title = "neu" });

            return Items;
        }
    }
}
