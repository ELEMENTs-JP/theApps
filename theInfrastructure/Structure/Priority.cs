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
            if (!string.IsNullOrEmpty(Prio))
            {
                if (Prio.IsNumeric() == false)
                {
                    Prio = DefaultPriorities().Find(se => se.Title == Prio).ID;
                }
            }

            switch (Prio)
            {
                case "5":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.rot);
                    }
                case "3":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.orange);
                    }
                case "1":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.gelb);
                    }
                case "0":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.marine);
                    }
                case "-1":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.grün);
                    }
                case "-3":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.violett);
                    }
                case "-5":
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.grau);
                    }
                default:
                    {
                        return Helper.ColorByDefaultColor(Helper.DefaultColor.weiss);
                    }
            }
        }

    }
}
