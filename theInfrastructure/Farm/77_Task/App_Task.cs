using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Task : BaseApp, IApp
    {
        public App_Task()
        { 
            Name = "Task-Management";

            // ItemTypes 
            this.ItemTypes.Add(new ItemType_Task());
        }
    }
}
