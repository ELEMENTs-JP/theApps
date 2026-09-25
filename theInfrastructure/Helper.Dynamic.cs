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
            new ComponentDefinition
            {
                Name = "DateTime",
                Namespace = "theComponents",
                ClassName = "DateTimeComp",
                CSS = " col-12 my-2",
                Group = "Date & Time"
            },

            // Apps & ItemTypes
            new ComponentDefinition
            {
                Name = "Apps",
                Namespace = "theComponents",
                ClassName = "AppsDisplayList",
                CSS = " col-12 my-2",
                Group = "Apps & ItemTypes"
            },
               new ComponentDefinition
            {
                Name = "ItemTypes",
                Namespace = "theComponents",
                ClassName = "ItemTypesDisplayList",
                CSS = " col-12 my-2",
                Group = "Apps & ItemTypes"
            },

            // Apps 
            new ComponentDefinition
            {
                Name = "Priority Chart",
                Namespace = "theComponents",
                ClassName = "PriorityChart",
                CSS = " col-12 my-2",
                Group = "Charts"
            },
        };



    }
}
