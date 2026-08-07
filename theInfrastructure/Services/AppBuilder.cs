using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public class AppBuilder
    {
        public AppBuilder() { }

        public static AppBuilder Create() { return new AppBuilder(); }

        public IApp BuildApp(string Name)
        {
            switch (Name)
            {
                case "Farm":
                    {
                        return new AppFarm();
                    }
                case "Task":
                    {
                        return new AppTask();
                    }
                default:
                    {
                        return new AppTask();
                    }
            }
        }
    }
}
