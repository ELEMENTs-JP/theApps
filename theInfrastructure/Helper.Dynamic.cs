using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        public static readonly HashSet<ComponentDefinition> ControlDefinitions = new()
        {
            // Test 
            new ComponentDefinition
            {
                Name = "Test UI",
                Namespace = "theInfrastructure",
                ClassName = "SimpleUICtl",
                CSS = " col-12 my-2",
                Group = "Test"
            },

            // Date 
            new ComponentDefinition
            {
                Name = "Date",
                Namespace = "theComponents",
                ClassName = "DateComp",
                CSS = " col-12 my-2",
                Group = "Date & Time"
            },
               new ComponentDefinition
            {
                Name = "Time",
                Namespace = "theComponents",
                ClassName = "TimeComp",
                CSS = " col-12 my-2",
                Group = "Date & Time"
            },
        };



    }
}
