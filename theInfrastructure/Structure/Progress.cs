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

        public static string GetProgressHexColor(int progress)
        {
            if (progress == 0) // 0 
            { return Helper.GetColor(GeneralColor.RedDark).HEX; }
            else if (progress >= 1 && progress <= 10) // 1 - 10 
            { return Helper.GetColor(GeneralColor.Red).HEX; }
            else if (progress > 10 && progress <= 25) // 11 - 25 
            { return Helper.GetColor(GeneralColor.RedLight).HEX; }

            else if (progress > 25 && progress <= 33) // 26 - 33 
            { return Helper.GetColor(GeneralColor.Orange).HEX; }

            else if (progress > 33 && progress <= 40) // 34 - 40
            { return Helper.GetColor(GeneralColor.Yellow).HEX; }

            else if (progress > 40 && progress <= 50) // 41 - 50 
            { return Helper.GetColor(GeneralColor.BlueDark).HEX; }

            else if (progress > 50 && progress <= 66) // 51 - 66 
            { return Helper.GetColor(GeneralColor.Blue).HEX; }

            else if (progress > 66 && progress <= 75) // 67 - 75 
            { return Helper.GetColor(GeneralColor.BlueLight).HEX; }

            else if (progress > 75 && progress <= 80) // 76 - 80 
            { return Helper.GetColor(GeneralColor.GreenLight).HEX; }

            else if (progress > 80 && progress <= 90) // 81 - 90 
            { return Helper.GetColor(GeneralColor.Green).HEX; }

            else if (progress > 90 && progress <= 97) // 91 - 97 
            { return Helper.GetColor(GeneralColor.Green).HEX; }

            else if (progress > 97 && progress <= 99) // 97 - 99 
            { return Helper.GetColor(GeneralColor.GreenDark).HEX; }

            else if (progress == 100)
            { return Helper.GetColor(GeneralColor.GreenDark).HEX; }

            else
            { return Helper.GetColor(GeneralColor.Silver).HEX; }
        }
    }
}
