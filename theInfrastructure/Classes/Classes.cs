using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text;

namespace theInfrastructure
{




    public class EventRenderItem
    {
        public CalendarEvent Event { get; set; } = default!;
        public int RowStart { get; set; }
        public int RowSpan { get; set; }
        public int ColumnIndex { get; set; }
        public int MaxColumns { get; set; }

        public string LeftPositionCss => $"{(ColumnIndex * 100.0 / MaxColumns):F2}%";
        public string WidthCss => $"{(100.0 / MaxColumns):F2}%";
    }
    public class CalendarEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string BackgroundColor { get; set; } = "#e7f1ff";
    }


    public class AudioEvent
    {
        public double CurrentTime { get; set; }
        public double Duration { get; set; }
        public double Volume { get; set; }
        public bool Muted { get; set; }
        public bool Ended { get; set; }
    }


    public enum GeneralColor
    {
        NULL = 0,

        GreenLight = 10,
        Green = 11,
        GreenDark = 12,

        RedLight = 30,
        Red = 31,
        RedDark = 32,

        BlueLight = 40,
        Blue = 41,
        BlueDark = 42,

        PinkLight = 51,
        Pink = 52,
        PinkDark = 53,

        ViolettLight = 61,
        Violett = 62,
        ViolettDark = 63,


        YellowLight = 71,
        Yellow = 72,
        YellowDark = 73,

        TurquoiseLight = 81,
        Turquoise = 82,
        TurquoiseDark = 83,

        RoseLight = 91,
        Rose = 92,
        RoseDark = 93,

        OrangeLight = 101,
        Orange = 102,
        OrangeDark = 103,

        // SignalRot = 12,

        Light = 201,
        Silver = 202,
        Grey = 203,
        Gray30 = 204,
        Gray50 = 205,
        Gray70 = 206,
        Dark = 207,
        Deep = 208,
        Black = 209,
    }
    public interface IColor
    {
        GeneralColor Color { get; set; }
        string HEX { get; set; }
    }
    public class tspColor : IColor
    {
        public GeneralColor Color { get; set; } = GeneralColor.NULL;
        public string HEX { get; set; } = string.Empty;
    }
    public class UrlAnalysis
    {
        public bool IsDevelopment { get; set; }
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Scheme { get; set; } = string.Empty;
        public string[] Segments { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Prüft, ob ein bestimmter Suchbegriff in mindestens einem der Segmente enthalten ist.
        /// </summary>
        /// <param name="searchTerm">Der zu suchende String.</param>
        /// <param name="comparison">Optional: Die Art des String-Vergleichs (Standard: Ignoriert Groß-/Kleinschreibung).</param>
        /// <returns>True, wenn der Suchbegriff in irgendeinem Segment gefunden wurde.</returns>
        public bool ContainsInSegments(string searchTerm, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(searchTerm) || Segments == null)
            {
                return false;
            }

            return Segments.Any(segment => segment.Contains(searchTerm, comparison));
        }

        public static UrlAnalysis AnalyzeUrl(NavigationManager nm)
        {
            Uri uri = new Uri(nm.Uri);

            // Bestimmung, ob es sich um eine Entwicklungsumgebung handelt
            bool isDev = uri.IsLoopback || uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            // Säuberung des Pfades von Query-Parametern und Aufteilung in Segmente
            string relativePath = nm.ToBaseRelativePath(nm.Uri);
            string pathOnly = relativePath.Split('?')[0];
            string[] cleanSegments = pathOnly.Split('/', StringSplitOptions.RemoveEmptyEntries);

            return new UrlAnalysis
            {
                IsDevelopment = isDev,
                Host = uri.Host,
                Port = uri.Port,
                Scheme = uri.Scheme,
                Segments = cleanSegments
            };
        }
    }

    public static class Sections
    {
        // SectionOutlet, SectionContent 
        public static readonly object ModalOutlet = new();
    }
    public interface IDataValue
    {
        IField Field { get; set; }
        string Value { get; set; }

    }
    public class DataValue : IDataValue
    {
        public DataValue(IField field, string value)
        {
            Field = field;
            Value = value;
        }

        public IField Field { get; set; }
        public string Value { get; set; } = string.Empty;
    }



    public class QueryResult : IQueryResult
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Guid GUID { get; set; } = Guid.Empty;

        public List<IDTO> Items { get; set; } = new();

        public void Validate()
        {
            if (Status != "OK")
            {
                Console.WriteLine("FAIL: " + Message);
            }
        }
    }
    public class ItemHistory
    {
        public static ItemHistory Empty(string value) => new ItemHistory { OldValue = value, NewValue = string.Empty, ChangeDate = DateTime.Now };
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public DateTime ChangeDate { get; set; } = DateTime.Now;

    }
    public class ItemProperty
    {
        public static ItemProperty Empty(string value) => new ItemProperty { Property = value, Value = string.Empty };
        public string Property { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class Metadata
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime EditedAt { get; set; } = DateTime.Now.Date;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public Guid EditedBy { get; set; } = Guid.Empty;
        public string Creator { get; set; } = string.Empty;
        public string Editor { get; set; } = string.Empty;

    }

    public class FilterParameter : IFilterParameter
    {
        // Suche 
        public string Matchcode { get; set; } = string.Empty;

        // Filterung 
        public List<KeyValuePair<string, string>> Parameters { get; set; } = new();

        // Sortierung 
        public string SortColumn { get; set; } = string.Empty;
        public ListSortDirection? Direction { get; set; } = null;

        // Gruppierung 
        public string GroupColumn { get; set; } = string.Empty;
    }
    public class QueryParameter : IQueryParameter
    {
        // Identify 
        public Guid GUID { get; set; } = Guid.NewGuid();
        public Guid MasterGUID { get; set; } 
        public string ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Matchcode { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public Guid UserGUID { get; set; } = Guid.Empty;
        public string UserName { get; set; } = string.Empty;
        public static IQueryParameter Default(string title)
        {
            IQueryParameter qp = new QueryParameter()
            {
                GUID = Guid.NewGuid(),
                Title = title
            };

            return qp;
        }

        public IEnumerable<string> ItemTypeExcludes { get; set; }

        // Validation 
        public string Message { get; set; } = string.Empty;
        public bool Validate()
        {
            if (this.MasterGUID == Guid.Empty)
            {
                this.Message = "Master GUID is Empty";
                return false;
            }
            return true;
        }
    }



}
