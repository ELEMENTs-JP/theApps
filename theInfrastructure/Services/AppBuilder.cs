using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure.Services
{
    public class AppBuilder
    {
        public AppBuilder() { }

        public static AppBuilder Create() { return new AppBuilder(); }

        public List<IApp> GetAllApps()
        { 
            List<IApp> apps = new List<IApp>();

            return apps;
        }


    }

    public enum AppTyp
    { 
        NULL = 0,

        
    }
}
