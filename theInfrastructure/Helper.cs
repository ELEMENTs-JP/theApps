using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace theInfrastructure
{
    public static class Helper
    {
       

        public static List<IDTO> InitDropDown(IField field)
        {
            List<IDTO> Items = new List<IDTO>();

            if (field.Typ == FieldTyp.DropDown)
            {

            }
            else if (field.Typ == FieldTyp.Priority)
            {
                Items.Add(new DTO() { ID = "9", Title = "Geschäftskritisch" });
                Items.Add(new DTO() { ID = "7", Title = "kritisch" });
                Items.Add(new DTO() { ID = "5", Title = "hoch" });
                Items.Add(new DTO() { ID = "3", Title = "mittel" });
                Items.Add(new DTO() { ID = "1", Title = "niedrig" });
                Items.Add(new DTO() { ID = "0", Title = "irrelevant" });
            }
            else if (field.Typ == FieldTyp.Progress)
            {
                Items.Add(new DTO() { ID = "9", Title = "100 %" });
                Items.Add(new DTO() { ID = "7", Title = "70 %" });
                Items.Add(new DTO() { ID = "5", Title = "50 %" });
                Items.Add(new DTO() { ID = "3", Title = "30 %" });
                Items.Add(new DTO() { ID = "1", Title = "10 %" });
                Items.Add(new DTO() { ID = "0", Title = "0 %" });
            }
            else if (field.Typ == FieldTyp.Status)
            {
                Items.Add(new DTO() { ID = "9", Title = "abgeschlossen" });
                Items.Add(new DTO() { ID = "7", Title = "zurückgestellt" });
                Items.Add(new DTO() { ID = "5", Title = "in Arbeit" });
                Items.Add(new DTO() { ID = "3", Title = "in Vorbereitung" });
                Items.Add(new DTO() { ID = "1", Title = "in Planung" });
                Items.Add(new DTO() { ID = "0", Title = "neu" });
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
                Items.Add(new DTO() { ID = "Number", Title = "Number" });
                Items.Add(new DTO() { ID = "Integer", Title = "Integer" });
                Items.Add(new DTO() { ID = "Money", Title = "Money" });
                Items.Add(new DTO() { ID = "Email", Title = "Email" });
                Items.Add(new DTO() { ID = "Url", Title = "Url" });
                Items.Add(new DTO() { ID = "DateTime", Title = "DateTime" });
                Items.Add(new DTO() { ID = "Date", Title = "Date" });
                Items.Add(new DTO() { ID = "Time", Title = "Time" });
            }

            return Items;
        }
        public static long MaxFileSize(int defaultValue = 10)
        {
            long defaultFileSizeInBytes = 1024 * 1024 * defaultValue;
            return defaultFileSizeInBytes;
        }
        public static Guid ToSecureGUID(this object text)
        {
            try
            {
                if (text == null)
                    return Guid.Empty;

                if (CheckGUID(text.ToString()))
                {
                    return new Guid(text.ToSecureString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : ToSecureGUID : " + ex.Message);
            }

            return Guid.Empty;
        }
        public static bool CheckGUID(string text)
        {
            return text != null
                   && text.Length == 36
                   && Guid.TryParseExact(text, "D", out _);
        }
        // Mail 
        public static bool IsMailFormat(this string mail)
        {
            if (mail.ToLower().Contains("@".ToLower()))
            {
                if (mail.ToLower().Contains(".".ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        // Navigation 
        public static void NavToApp(this NavigationManager nm, string App)
        {
            nm.NavigateTo("/App/" + App , false);
        }
        public static void NavToItem(this NavigationManager nm, string ItemType, Guid GUID)
        {
            nm.NavigateTo("/Item/" + ItemType + "/" + GUID.ToString(), false);
        }
        public static void NavToLibrary(this NavigationManager nm, string ItemType)
        {
            nm.NavigateTo("/Items/" + ItemType, false);
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
        private static bool SafeConvertible(IConvertible convertible)
        {
            if (convertible == null)
                return false;

            switch (convertible.GetTypeCode())
            {
                case TypeCode.Boolean:
                return (bool)convertible;

                case TypeCode.String:
                return bool.TryParse((string)convertible, out var result)
                       && result;

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                return convertible.ToInt64(null) != 0;

                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                return convertible.ToDouble(null) != 0;

                default:
                return false;
            }
        }
        public static bool ToSecureBool(this object? obj)
        {
            return obj switch
            {
                null => false,
                bool b => b,
                int i => i != 0,
                string s when bool.TryParse(s, out var b) => b,
                string s when int.TryParse(s, out var i) => i != 0,
                IConvertible c => SafeConvertible(c), // Fallback für Spezialtypen
                _ => false
            };
        }
        public static string ToDate(this object? obj)
        {
            string val = obj.ToSecureString();

            if (val.Contains("T"))
            {
                val = val.SplitGetFirst("T");
            }

            return val;
        }
        public static string SplitGetFirst(this string text, string separator = "-")
        {
            if (!text.Contains(separator))
            {
                return text;
            }

            List<char> cs = new List<char>();
            if (separator.Length > 1)
            {
                foreach (char c in separator)
                {
                    cs.Add(c);
                }
                return text.Split(cs.ToArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            }
            return text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)[0];
        }
        public static string SplitGetLast(this string text, string separator = ".")
        {
            try
            {
                if (text == null)
                {
                    return string.Empty;
                }

                string[] arr = text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                return arr[arr.Length - 1];
            }
            catch
            {
                return string.Empty;
            }
        }
        public static DateTime ToSecureDateTime(this object text)
        {
            try
            {
                if (text == null)
                    return DateTime.Now;

                if (string.IsNullOrEmpty(text.ToString()))
                {
                    return DateTime.MinValue;
                }

                if (text.ToSecureString().ToLower().Contains("T".ToLower()))
                {
                    string date = text.ToSecureString().SplitGetFirst("T");
                    string time = text.ToSecureString().SplitGetLast("T");

                    // check 
                    time = (time.SplitGetFirst(":").Length == 1) ? ("0" + time) : time;

                    DateTime newDate = Convert.ToDateTime(date + "T" + time);
                    return newDate;
                }

                return Convert.ToDateTime(text.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FAIL : DateTime Conversion : " + ex.Message);
                return DateTime.Now;
            }
        }
        public static decimal ToSecureDecimal(this object text, decimal defaultIfNullOrEmpty = 0m, decimal defaultIfZero = 0m)
        {
            try
            {
                if (text == null)
                {
                    return defaultIfNullOrEmpty;
                }

                if (string.IsNullOrEmpty(text.ToSecureString()))
                {
                    return defaultIfNullOrEmpty;
                }

                if (decimal.TryParse(text.ToSecureString(), out decimal result))
                {
                    return result == 0 ? defaultIfZero : result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FAIL : ToSecureDecimal : " + ex.Message);
            }

            return defaultIfNullOrEmpty;
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

        private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string ToShortCode(this Guid guid, int length = 5)
        {
            if (length < 1 || length > 6)
                throw new ArgumentOutOfRangeException(nameof(length));

            // SHA256 über die GUID bilden
            byte[] hash = SHA256.HashData(guid.ToByteArray());

            // Erste 4 Bytes als UInt32 verwenden
            uint value = BitConverter.ToUInt32(hash, 0);

            // Maximale Anzahl für die gewünschte Länge
            uint max = (uint)Math.Pow(62, length);

            value %= max;

            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Insert(0, Base62Chars[(int)(value % 62)]);
                value /= 62;
            }

            return result.ToString();
        }

        public static int ToSecureInt(this object text, int defaultValue = 0)
        {
            if (text == null || text == DBNull.Value)
                return defaultValue;

            // Falls das Objekt bereits ein numerischer Typ ist (z.B. double oder float)
            // fangen wir NaN hier direkt ab, bevor die Konvertierung fehlschlägt.
            if (text is double d && double.IsNaN(d))
                return defaultValue;
            if (text is float f && float.IsNaN(f))
                return defaultValue;

            string stringValue = text.ToString()?.Trim();

            if (string.IsNullOrEmpty(stringValue) || stringValue.Equals("NaN", StringComparison.OrdinalIgnoreCase))
                return defaultValue;

            // TryParse ist deutlich schneller als Convert.ToInt32 + Catch
            if (int.TryParse(stringValue, out int result))
            {
                return result;
            }

            // Normalisierung: Ersetzt das Komma durch einen Punkt, damit InvariantCulture 
            // die Fließkommazahl unabhängig von den Server-Regionaleinstellungen korrekt liest.
            string normalizedValue = stringValue.Replace(',', '.');

            if (double.TryParse(normalizedValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double dblResult))
            {
                return (int)Math.Round(dblResult);
            }

            return defaultValue;
        }
        public static string ToSecureString(this object? value, string placeholder = "")
        {
            // 1. Direktes Null-Handling (Pattern Matching)
            if (value is null)
                return placeholder;

            // 2. Performance-Boost: Falls es bereits ein String ist, Cast statt ToString()
            if (value is string s)
            {
                return string.IsNullOrWhiteSpace(s) ? placeholder : s;
            }

            // 3. Sicherer Aufruf von ToString() (nur einmal!)
            try
            {
                string? result = value.ToString();
                return string.IsNullOrWhiteSpace(result) ? placeholder : result;
            }
            catch (Exception ex)
            {
                // Fehler nur im Debug-Modus loggen
                System.Diagnostics.Debug.WriteLine($"ToSecureString FAIL: {ex.Message}");
                return placeholder; // Konsistent zum Null-Fall den Placeholder zurückgeben
            }
        }
  

        public static bool IsGuid(string input)
        {
            // Schneller Null- und Längencheck (Standard-GUIDs haben inkl. Bindestrichen 36 Zeichen)
            if (input == null || input.Length != 36)
                return false;

            // .NET 10 nutzt hier intern ReadOnlySpan<char> unter der Haube -> 0 Heap-Allokationen
            return Guid.TryParse(input, out _);
        }

        public static string ToSQLiteDateTimeString(this DateTime dt)
        {
            // 2023-06-19T09:00:00 

            string year = dt.Year.ToString();
            string month = (dt.Month <= 9) ? "0" + dt.Month.ToString() : dt.Month.ToString();
            string day = (dt.Day <= 9) ? "0" + dt.Day.ToString() : dt.Day.ToString();
            string hour = (dt.Hour <= 9) ? "0" + dt.Hour.ToString() : dt.Hour.ToString();
            string minute = (dt.Minute <= 9) ? "0" + dt.Minute.ToString() : dt.Minute.ToString();
            string second = (dt.Second <= 9) ? "0" + dt.Second.ToString() : dt.Second.ToString();

            string SQLite = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second;

            System.Diagnostics.Debug.WriteLine("SQLite DateConversion: " + SQLite);

            return SQLite;
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
