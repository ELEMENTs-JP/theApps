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

            Items.Add(new DTO() { ["Icon"] = "ti ti-rectangle", ID = "0", Title = "0 %", Content = "neu / erstellt" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-tallymark-1", ID = "10", Title = "10 %", Content = "in Vorbereitung" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-tallymark-2", ID = "25", Title = "25 %", Content = "Arbeiten vollem Gange" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-tallymark-3", ID = "33", Title = "33 %", Content = "erste Fortschritte" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-chevron-right", ID = "40", Title = "40 %", Content = "erste Ergebnisse vorzeigbar" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-chevrons-right", ID = "50", Title = "50 %", Content = "es ist Halbzeit" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-player-play", ID = "66", Title = "66 %", Content = "weitere Fortschritte" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-player-skip-forward", ID = "76", Title = "75 %", Content = "vorzeigbare Ergebnisse" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-chevron-right", ID = "80", Title = "80 %", Content = "Endspurt gestartet" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-player-track-next", ID = "90", Title = "90 %", Content = "Optimierungsarbeiten" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-circle-chevrons-right", ID = "97", Title = "97 %", Content = "Abschluss begonnen" });
            Items.Add(new DTO() { ["Icon"] = "ti ti-circle-check", ID = "100", Title = "100 %", Content = "Arbeiten fertiggestellt"});

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
