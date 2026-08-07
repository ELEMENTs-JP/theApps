using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class AppTask : BaseApp, IApp
    {
        public AppTask()
        { 
            Name = "Task-Management";

            // ItemTypes 
            this.ItemTypes.Add(new ItemType_Task());
        }
    }
}
