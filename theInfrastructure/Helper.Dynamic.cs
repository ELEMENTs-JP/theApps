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
               new ComponentDefinition
            {
                Name = "Kalender",
                Namespace = "theComponents",
                ClassName = "CalendarMonthMiniComp",
                CSS = " col-12 my-2",
                Group = "Date & Time"
            },
               new ComponentDefinition
            {
                Name = "Jahr, Monat, Wochen u. Zeitübersicht",
                Namespace = "theComponents",
                ClassName = "YearMonthDayTimeProgress",
                CSS = " col-12 my-2",
                Group = "Date & Time",
                IsActive = true,
            },

            // Host 
            new ComponentDefinition
            {
                Name = "Willkommen",
                Namespace = "theComponents",
                ClassName = "WelcomeHost",
                CSS = " col-12 my-2",
                Group = "Host"
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

            // Charts 
            new ComponentDefinition
            {
                Name = "Priority Chart",
                Namespace = "theComponents",
                ClassName = "PriorityChart",
                CSS = " col-12 my-2",
                Group = "Charts"
            },

            // STRATEGYzer 
              new ComponentDefinition
            {
                Name = "Vision & Mission",
                Namespace = "theComponents",
                ClassName = "VisionMission",
                CSS = " col-12 my-2",
                Group = "Strategy"
            },

              // RUNer 
               new ComponentDefinition
            {
                Name = "RSS News Reader",
                Namespace = "theComponents",
                ClassName = "NewsRssReader",
                CSS = " col-12 my-2",
                Group = "Tools"
            },

            // News 
            new ComponentDefinition
            {
                Name = "Wirtschaft u. Finanzen",
                Namespace = "theComponents",
                ClassName = "NewsBusinessFinance",
                CSS = " col-12 my-2",
                Group = "News"
            },
            new ComponentDefinition
            {
                Name = "Globales und Märkte",
                Namespace = "theComponents",
                ClassName = "NewsGlobalMarkets",
                CSS = " col-12 my-2",
                Group = "News"
            },
              new ComponentDefinition
            {
                Name = "Wachstum und Trends",
                Namespace = "theComponents",
                ClassName = "NewsGrowthTrends",
                CSS = " col-12 my-2",
                Group = "News"
            },
               new ComponentDefinition
            {
                Name = "Politik",
                Namespace = "theComponents",
                ClassName = "NewsPolitic",
                CSS = " col-12 my-2",
                Group = "News"
            },
               new ComponentDefinition
            {
                Name = "Technology",
                Namespace = "theComponents",
                ClassName = "NewsTechnology",
                CSS = " col-12 my-2",
                Group = "News"
            },
        };



    }
}
