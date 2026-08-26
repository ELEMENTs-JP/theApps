using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class Priority
    {
        public static List<IDTO> DefaultPriorities()
        {
            List<IDTO> Items = new();

            Items.Add(new DTO() { ID = "5", Title = "Unternehmenskritisch" });
            Items.Add(new DTO() { ID = "3", Title = "Business Critical" });
            Items.Add(new DTO() { ID = "1", Title = "höher" });
            Items.Add(new DTO() { ID = "0", Title = "ausgeglichen" });
            Items.Add(new DTO() { ID = "-1", Title = "niedrig" });
            Items.Add(new DTO() { ID = "-3", Title = "niedriger" });
            Items.Add(new DTO() { ID = "-5", Title = "irrelevant" });

            return Items;
        }
        public static Icon IconByPriority(string Prio)
        {
            switch (Prio)
            {
                case "Unternehmenskritisch":
                    {
                        return Icon.Prio_Highest;
                    }
                case "Business Critical":
                    {
                        return Icon.Prio_Higher;
                    }
                case "höher":
                    {
                        return Icon.Prio_High;
                    }
                case "ausgeglichen":
                    {
                        return Icon.Prio_Middle;
                    }
                case "niedrig":
                    {
                        return Icon.Prio_Low;
                    }
                case "niedriger":
                    {
                        return Icon.Prio_Lower;
                    }
                case "irrelevant":
                    {
                        return Icon.Prio_Lowest;
                    }
                default:
                    {
                        return Icon.NULL;
                    }
            }
        }
        public static string ColorByPriority(string Prio)
        {
            switch (Prio)
            {
                case "5":
                    {
                        return "var(--tblr-red)";
                    }
                case "3":
                    {
                        return "var(--tblr-orange)";
                    }
                case "1":
                    {
                        return "var(--tblr-yellow)";
                    }
                case "0":
                    {
                        return "var(--tblr-blue)";
                    }
                case "-1":
                    {
                        return "var(--tblr-green)";
                    }
                case "-3":
                    {
                        return "var(--tblr-purple)";
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

    }
}
