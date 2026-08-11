using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    // App 
    public class App_Farm : BaseApp, IApp
    {
        public App_Farm()
        { 
            Name = "Farm";

            // ItemTypes 
            this.ItemTypes.Add(new ItemType_App());
            this.ItemTypes.Add(new ItemType_ItemType());
            this.ItemTypes.Add(new ItemType_Field());
        }

      
    }

}
