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

            Items.Add(new DTO() { ID = "9", Title = "Unternehmenskritisch" });
            Items.Add(new DTO() { ID = "7", Title = "Business Critical" });
            Items.Add(new DTO() { ID = "5", Title = "höher" });
            Items.Add(new DTO() { ID = "5", Title = "ausgeglichen" });
            Items.Add(new DTO() { ID = "3", Title = "niedrig" });
            Items.Add(new DTO() { ID = "1", Title = "niedriger" });
            Items.Add(new DTO() { ID = "0", Title = "irrelevant" });

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
                case "Unternehmenskritisch":
                    {
                        return "var(--tblr-red)";
                    }
                case "Business Critical":
                    {
                        return "var(--tblr-orange)";
                    }
                case "höher":
                    {
                        return "var(--tblr-yellow)";
                    }
                case "ausgeglichen":
                    {
                        return "var(--tblr-blue)";
                    }
                case "niedrig":
                    {
                        return "var(--tblr-green)";
                    }
                case "niedriger":
                    {
                        return "var(--tblr-purple)";
                    }
                case "irrelevant":
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
