using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Template : BaseApp, IApp
    {
        public App_Template(string Name)
        { 
            this.Name = Name;
        }
    }

}
