using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class Progress
    {
        public static List<IDTO> DefaultProgress()
        {
            List<IDTO> Items = new();

            Items.Add(new DTO() { ID = "100", Title = "100 %" });
            Items.Add(new DTO() { ID = "70", Title = "70 %" });
            Items.Add(new DTO() { ID = "50", Title = "50 %" });
            Items.Add(new DTO() { ID = "30", Title = "30 %" });
            Items.Add(new DTO() { ID = "10", Title = "10 %" });
            Items.Add(new DTO() { ID = "0", Title = "0 %" });

            return Items;
        }
    }
}
