using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public record AppInfo(string Name, string Title);
    public record ItemTypeInfo(string Name, string Title);
    public record FieldInfo(string Name, string Typ);
    public record ItemTypeConnection(string Parent, string Child);
    public interface ITemplateApp
    {
        AppInfo GetApp();
        List<ItemTypeInfo> GetItemTypes();
    }

    public class TemplateApp : ITemplateApp
    {
        public TemplateApp()
        {
            // Constructor implementation
        }

        public virtual AppInfo GetApp()
        {
            return new AppInfo("Template", "Template App");
        }
        public virtual List<ItemTypeInfo> GetItemTypes()
        {
            List<ItemTypeInfo> types = new List<ItemTypeInfo>();

            //types.Add(new ItemTypeInfo("Template", "Template Item"));

            return types;
        }
        public virtual List<ItemTypeConnection> GetItemTypeConnections()
        {
            List<ItemTypeConnection> connections = new List<ItemTypeConnection>();

            //connections.Add(new ItemTypeConnection("Template", "Template"));

            return connections;
        }
        public virtual List<FieldInfo> GetFields(string ItemType)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            //if (ItemType == "Template")
            //{
            //    fields.Add(new FieldInfo("Template", "string"));
            //    fields.Add(new FieldInfo("Template", "string"));
            //}

            return fields;
        }
    }

    public static class TemplateAppFactory
    {
        public static TemplateApp CreateTemplateApp(TemplateAppType appType)
        {
            switch (appType)
            {
                case TemplateAppType.BusinessModeler:
                    { 
                        return new TApp_BusinessModeler();
                    }
                
                default:
                    { 
                        throw new ArgumentException($"Unknown app type: {appType}");
                    }
            }
        }
        public static List<ITemplateApp> GetAllTemplateApps()
        {
            return new List<ITemplateApp>
            {
                new TApp_BusinessModeler(),
                new TApp_Strategyzer(),
                new TApp_Worker(),
                new TApp_Marketing(),
            };
        }
    }

    public enum TemplateAppType 
    { 
        NULL = 0,
        BusinessModeler = 1,
    }

}
