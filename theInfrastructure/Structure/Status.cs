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

        public static Icon IconByStatus(string Status)
        {
            switch (Status)
            {
                case "abgeschlossen":
                    {
                        return Icon.Status_Finished;
                    }
                case "zurückgestellt":
                    {
                        return Icon.Status_onHold;
                    }
                case "in Arbeit":
                    {
                        return Icon.Status_inWork;
                    }
                case "in Vorbereitung":
                    {
                        return Icon.Status_inPreparation;
                    }
                case "in Planung":
                    {
                        return Icon.Status_inPlan;
                    }
                case "neu":
                    {
                        return Icon.Status_New;
                    }
                default:
                    {
                        return Icon.NULL;
                    }
            }
        }
        public static string ColorByStatus(string Prio)
        {
            switch (Prio)
            {
                case "0":
                    {
                        return "var(--tblr-red)";
                    }
                case "1":
                    {
                        return "var(--tblr-orange)";
                    }
                case "3":
                    {
                        return "var(--tblr-yellow)";
                    }
                case "5":
                    {
                        return "var(--tblr-blue)";
                    }
                case "7":
                    {
                        return "var(--tblr-purple)";
                    }
                case "9":
                    {
                        return "var(--tblr-green)";
                    }
                case "-5":
                    {
                        return "var(--tblr-gray-500)";
                    }
                default:
                    {
                        return "#fff";
                    }
            }
        }
        public static string ColorByStatusValue(string Value)
        {
            switch (Value)
            {
                case "abgeschlossen":
                    {
                        return "var(--tblr-green)";
                    }
                case "zurückgestellt":
                    {
                        return "var(--tblr-purple)";
                    }
                case "in Arbeit":
                    {
                        return "var(--tblr-blue)";
                    }
                case "in Vorbereitung":
                    {
                        return "var(--tblr-yellow)";
                    }
                case "in Planung":
                    {
                        return "var(--tblr-orange)";
                    }
                case "neu":
                    {
                        return "var(--tblr-red)";
                    }
                //case "-5":
                //    {
                //        return "var(--tblr-gray-500)";
                //    }
                default:
                    {
                        return "#fff";
                    }
            }
        }

    }
}
