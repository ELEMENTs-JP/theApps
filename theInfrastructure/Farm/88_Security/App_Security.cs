using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Security : BaseApp, IApp
    {
        public App_Security()
        { 
            Name = "Security";

            // ItemTypes 
            this.ItemTypes.Add(new ItemType_User());
        }
    }
}
