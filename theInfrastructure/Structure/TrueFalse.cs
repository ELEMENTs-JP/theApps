using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class TrueFalse
    {
        public static Icon IconByValue(string truefalse)
        {
            switch (truefalse)
            {
                case "true":
                    {
                        return Icon.YES;
                    }
                case "false":
                    {
                        return Icon.NO;
                    }
                case "yes":
                    {
                        return Icon.YES;
                    }
                case "no":
                    {
                        return Icon.NO;
                    }
                case "ja":
                    {
                        return Icon.YES;
                    }
                case "nein":
                    {
                        return Icon.NO;
                    }
                default:
                    {
                        return Icon.Invariant;
                    }
            }
        }
        public static string ColorByValue(string truefalse)
        {
            switch (truefalse)
            {
                case "true":
                    {
                        return "var(--tblr-green)";
                    }
                case "false":
                    {
                        return "var(--tblr-red)";
                    }
                case "yes":
                    {
                        return "var(--tblr-green)";
                    }
                case "no":
                    {
                        return "var(--tblr-red)";
                    }
                case "ja":
                    {
                        return "var(--tblr-green)";
                    }
                case "nein":
                    {
                        return "var(--tblr-red)";
                    }
                default:
                    {
                        return "#fff";
                    }
            }
        }

    }
}
