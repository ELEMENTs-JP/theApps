using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace theInfrastructure
{


    public static partial class Helper
    {
        // Static Fields 
        public static string? SplitGetIndex(this string input, string separator, int index)
        {
            ArgumentNullException.ThrowIfNull(input, nameof(input));
            ArgumentNullException.ThrowIfNull(separator, nameof(separator));

            if (index < 0)
            {
                return null;
            }

            // Spezialfall: Leeres Trennzeichen
            if (separator.Length == 0)
            {
                return index == 0 ? input : null;
            }

            ReadOnlySpan<char> span = input.AsSpan();
            ReadOnlySpan<char> sepSpan = separator.AsSpan();

            int currentIndex = 0;
            int startIndex = 0;

            while (true)
            {
                int matchIndex = span.Slice(startIndex).IndexOf(sepSpan, StringComparison.Ordinal);

                if (matchIndex == -1)
                {
                    // Letztes Segment erreicht
                    if (currentIndex == index)
                    {
                        return span.Slice(startIndex).ToString();
                    }
                    return null;
                }

                if (currentIndex == index)
                {
                    return span.Slice(startIndex, matchIndex).ToString();
                }

                currentIndex++;
                startIndex += matchIndex + sepSpan.Length;
            }
        }
    

        public static IReadOnlyDictionary<string, string> GetPropertyDefaultValues()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Sort", "0" },
                { "Zone", "Default" }
            };

        }


        public static (List<IDTO> Matching, List<IDTO> NonMatching) SplitByProperty(this IEnumerable<IDTO> items,
                    Func<IDTO, string> propertySelector, string targetValue)
        {
            // Aufruf: var (gruppeA, restliche) = SplitByProperty(Items, x => x.Category, "A"); 

            var matching = new List<IDTO>();
            var nonMatching = new List<IDTO>();

            foreach (var item in items)
            {
                if (string.Equals(propertySelector(item), targetValue, StringComparison.OrdinalIgnoreCase))
                {
                    matching.Add(item);
                }
                else
                {
                    nonMatching.Add(item);
                }
            }

            return (matching, nonMatching);
        }
        public static bool MatchesPropertySearch(this string jsonString, string query)
        {
            if (string.IsNullOrWhiteSpace(jsonString) || string.IsNullOrWhiteSpace(query))
                return false;

            var parts = query.Split(':', 2);

            // Falls kein Doppelpunkt enthalten ist: Normale Volltextsuche
            if (parts.Length < 2)
            {
                return jsonString.Contains(query, StringComparison.OrdinalIgnoreCase);
            }

            string targetProperty = parts[0].Trim();
            string searchValue = parts[1].Trim();

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = doc.RootElement;

                    // Stellt sicher, dass das Wurzel-Element wirklich ein JSON-Objekt { ... } ist
                    if (root.ValueKind != JsonValueKind.Object)
                    {
                        // Falls es kein Objekt ist, Fallback auf einfachen String-Vergleich
                        return jsonString.Contains(query, StringComparison.OrdinalIgnoreCase);
                    }

                    foreach (JsonProperty property in root.EnumerateObject())
                    {
                        if (property.Name.Equals(targetProperty, StringComparison.OrdinalIgnoreCase))
                        {
                            // Wert als String holen – egal ob String, Zahl oder Boolean
                            string? val = property.Value.ValueKind switch
                            {
                                JsonValueKind.String => property.Value.GetString(),
                                JsonValueKind.Null => null,
                                JsonValueKind.Undefined => null,
                                _ => property.Value.GetRawText() // Für Numbers, Booleans etc.
                            };

                            return val != null && val.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Fängt JsonException sowie unerwartete Formatfehler sicher ab
                return jsonString.Contains(query, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public static string ToSecureEnumString(this AssociationTyp typ)
        {
            return typ switch
            {
                AssociationTyp.NULL => string.Empty,
                _ => typ.ToString()
            };
        }
        // Reflection 
        public static Type SpecificType(string assemblyName = "", string className = "")
        {
            try
            {
                Assembly? assembly = Assembly.Load(assemblyName);
                if (assembly == null)
                {
                    new Exception("tSP: Assembly is null");
                }
                if (assembly != null)
                {
                    Type? type = assembly.GetTypes().Where(se => se.Name == className).FirstOrDefault();
                    if (type == null)
                    {
                        new Exception("tSP: Control is null");
                    }
                    if (type != null)
                    {
                        return type;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static List<IDTO> GetDefaultAssociations()
        {
            List<IDTO> idtos = new List<IDTO>();
            foreach (AssociationTyp at in Enum.GetValues(typeof(AssociationTyp)))
            {
                idtos.Add(new DTO { ID = at.ToString(), Title = at.ToString() });
            }

            return idtos;
        }
        public static string GetGlassClass(this LayoutConfiguration config)
        {
            if (config.Glass == true)
            {
                return "  glass  ";
            }

            return string.Empty;
        }
        public static string GetAkzentStyle(this LayoutConfiguration config)
        {
            if (config.Akzente == true)
            {
                return "  border-top: 3px solid #ffffff !important;  ";
            }

            return string.Empty;
        }
        public static TToEnum ToEnum<TToEnum>(this string value, bool ignoreCase = true) where TToEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "String darf nicht leer sein.");
            }

            if (Enum.TryParse<TToEnum>(value, ignoreCase, out var result))
            {
                return result;
            }

            throw new ArgumentException($"Der Wert '{value}' konnte nicht in das Enum '{typeof(TToEnum).Name}' konvertiert werden.");
        }

        public static TToEnum ToEnumOrDefault<TToEnum>(this string value, TToEnum defaultValue = default, bool ignoreCase = true)
            where TToEnum : struct, Enum
        {
            return Enum.TryParse<TToEnum>(value, ignoreCase, out var result) ? result : defaultValue;
        }

        public static IList<T> FromTo<T>(this IList<T> list, int first, int last)
        {
            if (list == null || list.Count == 0)
                return new List<T>();

            if (first < 0 || last < 0 || first > last || first >= list.Count)
                return new List<T>();

            last = Math.Min(last, list.Count - 1);

            var result = new List<T>(last - first + 1);

            for (int i = first; i <= last; i++)
            {
                result.Add(list[i]);
            }

            return result;
        }

        public static async Task<List<IDTO>> InitDropDown(IField field, ISqlDatabaseService sql)
        {
            List<IDTO> Items = new List<IDTO>();

            if (field.Typ == FieldTyp.DropDown)
            {
                foreach (string val in field.Items)
                {
                    Items.Add(new DTO() { ID = val, Title = val });
                }
            }
            else if (field.Typ == FieldTyp.Priority)
            {
                Items = Priority.DefaultPriorities();
            }
            else if (field.Typ == FieldTyp.Progress)
            {
                Items = Progress.DefaultProgress();
            }
            else if (field.Typ == FieldTyp.Status)
            {
                Items = Status.DefaultStatus();
            }
            else if (field.Typ == FieldTyp.FieldTyp)
            {
                Items.Add(new DTO() { ID = "Text", Title = "Text" });
                Items.Add(new DTO() { ID = "TextArea", Title = "TextArea" });
                Items.Add(new DTO() { ID = "Html", Title = "Html" });
                Items.Add(new DTO() { ID = "DropDown", Title = "DropDown" });
                Items.Add(new DTO() { ID = "CheckBox", Title = "CheckBox" });
                Items.Add(new DTO() { ID = "Priority", Title = "Priority" });
                Items.Add(new DTO() { ID = "Status", Title = "Status" });
                Items.Add(new DTO() { ID = "Progress", Title = "Progress" });
                Items.Add(new DTO() { ID = "User", Title = "User" });
                Items.Add(new DTO() { ID = "Number", Title = "Number" });
                Items.Add(new DTO() { ID = "Integer", Title = "Integer" });
                Items.Add(new DTO() { ID = "Money", Title = "Money" });
                Items.Add(new DTO() { ID = "Email", Title = "Email" });
                Items.Add(new DTO() { ID = "Url", Title = "Url" });
                Items.Add(new DTO() { ID = "DateTime", Title = "DateTime" });
                Items.Add(new DTO() { ID = "Date", Title = "Date" });
                Items.Add(new DTO() { ID = "Time", Title = "Time" });
            }
            else if (field.Typ == FieldTyp.ItemTypeTyp)
            {
                Items.Add(new DTO() { ID = "Item", Title = "Item" });
                Items.Add(new DTO() { ID = "File", Title = "File" });
                Items.Add(new DTO() { ID = "Appointment", Title = "Appointment" });
                Items.Add(new DTO() { ID = "Hierarchy", Title = "Hierarchy" });
            }
            else if (field.Typ == FieldTyp.User)
            {
                IQueryParameter qp = new QueryParameter();
                qp.MasterGUID = sql.MasterGUID;
                qp.ItemType = "User";
                IQueryResult result = await sql.GetItems(qp);
                Items = result.Items;
            }
            else if (field.Typ == FieldTyp.ItemTypeList)
            {
                IQueryParameter qp = new QueryParameter();
                qp.MasterGUID = sql.MasterGUID;
                qp.ItemType = "ItemType";
                IQueryResult result = await sql.GetItems(qp);
                Items = result.Items;
            }
            else if (field.Typ == FieldTyp.AppList)
            {
                IQueryParameter qp = new QueryParameter();
                qp.MasterGUID = sql.MasterGUID;
                qp.ItemType = "App";
                IQueryResult result = await sql.GetItems(qp);
                Items = result.Items;
            }
            else if (field.Typ == FieldTyp.FunctionList)
            {
                Items.Add(new DTO() { ID = "Create", Title = "Create" });
                Items.Add(new DTO() { ID = "Read", Title = "Read" });
                Items.Add(new DTO() { ID = "Update", Title = "Update" });
                Items.Add(new DTO() { ID = "Delete", Title = "Delete" });
            }
            else if (field.Typ == FieldTyp.AssociationTyp)
            {
                Items.Add(new DTO() { ID = "Default", Title = "Default" });
                Items.Add(new DTO() { ID = "Parents", Title = "Parents" });
                Items.Add(new DTO() { ID = "Children", Title = "Children" });
                Items.Add(new DTO() { ID = "Related", Title = "Related" });
                Items.Add(new DTO() { ID = "Parallel", Title = "Parallel" });
            }

            return Items;
        }



        public static string GetClassByDevice(IField field)
        {

            if (field.OnDevice == DeviceDisplay.Tablet)
            {
                return " d-none d-md-table-cell ";
            }
            else if (field.OnDevice == DeviceDisplay.Desktop)
            {
                return " d-none d-lg-table-cell ";
            }

            return string.Empty;
        }
        public static void AddOrUpdate(this List<IDataValue> items, IDataValue value)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(value?.Field?.Title);

            int index = items.FindIndex(x => x?.Field?.Title == value.Field.Title);

            if (index >= 0)
                items[index] = value; // Ersetzen über Direct Index Assignment O(1)
            else
                items.Add(value);     // Hinzufügen O(1)
        }
        public static bool HasContent(this RenderFragment fragment)
        {
            try
            {
                if (fragment == null)
                    return false;

                var builder = new RenderTreeBuilder();
                fragment(builder);

                return builder.GetFrames().Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string GenerateName(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
                return $"input_{Guid.NewGuid():N}";

            var name = label.Trim().ToLowerInvariant();

            name = name
                .Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue")
                .Replace("ß", "ss");

            var chars = name
                .Select(c => char.IsLetterOrDigit(c) ? c : '_')
                .ToArray();

            name = new string(chars);

            while (name.Contains("__"))
                name = name.Replace("__", "_");

            return name.Trim('_');
        }
        public static string ToHtmlValue(this FieldTyp type)
        {
            return type switch
            {
                FieldTyp.Text => "text",
                FieldTyp.Password => "password",
                FieldTyp.Email => "email",
                FieldTyp.Number => "number",
                FieldTyp.Integer => "number",
                FieldTyp.Decimal => "number",
                FieldTyp.Money => "number",
                FieldTyp.Tel => "tel",
                FieldTyp.Url => "url",
                FieldTyp.Search => "search",
                FieldTyp.Date => "date",
                FieldTyp.Time => "time",
                FieldTyp.DateTime => "datetime-local",
                FieldTyp.Month => "month",
                FieldTyp.Week => "week",
                FieldTyp.Color => "color",
                FieldTyp.Range => "range",
                FieldTyp.Hidden => "hidden",
                _ => "text"
            };
        }
        public static string? GetStep(this FieldTyp type)
        {
            return type switch
            {
                FieldTyp.Integer => "1",
                FieldTyp.Decimal => "any",
                _ => null
            };
        }

        public static object MapProperties(object UI, object DB)
        {
            // check Objects 
            if (UI == null || DB == null)
                return default;

            // Check Type 
            Type _uiType = UI.GetType();
            Type _dbType = DB.GetType();
            if (_dbType.FullName != _uiType.FullName)
                return null;

            PropertyInfo[] properties = _dbType.GetProperties(
              BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pi in properties)
            {
                //check 
                if (pi == null)
                    continue;

                if (!pi.CanRead)
                    continue;

                // not mapping GUID 
                if (pi.Name.Equals("GUID", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (pi.Name.Equals("MasterGUID", StringComparison.OrdinalIgnoreCase))
                    continue;

                // Check for NotMapped 
                if (pi.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;


                // get old value 
                object uiValue = null;
                try
                {
                    uiValue = pi.GetValue(UI);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("FAIL : Map Properties by Reflection : " + ex.Message);
                }

                try
                {
                    // SAVE VALUE to dbObject : set new Value 
                    DB.GetType().GetProperty(pi.Name).SetValue(DB, uiValue);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("FAIL : Map Properties By Reflection 2 : " + ex.Message);
                }
            }

            return DB;
        }

        public static string GenerateShort(string itemType)
        {
            string result = string.Concat(itemType.Where(c => c >= 'A' && c <= 'Z'));

            if (result.Length == 2)
            {
                return result;
            }
            else if (result.Length > 2)
            {
                return result.Substring(0, 2);
            }
            else if (result.Length == 1)
            {
                return result + "Z";
            }
            else
            {
                return result + "AZ";
            }
        }

        // Numbers 
        public static int ExtractNumber(this string original, int fail = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(original))
                    return 0;

                char[] numbers = original.Where(c => Char.IsNumber(c)).ToArray();
                char[] digits = original.Where(c => Char.IsDigit(c)).ToArray();

                string number = new string(numbers);
                string digit = new string(digits);

                if (!string.IsNullOrEmpty(number))
                { return int.Parse(number); }

                if (!string.IsNullOrEmpty(number))
                { return int.Parse(digit); }

                return fail;
            }
            catch
            {
                return fail;
            }
        }

        // JSON Serialization 
        public static readonly JsonSerializerOptions JsonOption = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true // Ignoriert Groß-/Kleinschreibung beim Deserialisieren
        };

        public static string Serialize<T>(T obj)
        {
            if (obj == null)
                return string.Empty;
            return JsonSerializer.Serialize(obj, JsonOption);
        }
        public static T? Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;
            return JsonSerializer.Deserialize<T>(json, JsonOption);
        }

    }
}
