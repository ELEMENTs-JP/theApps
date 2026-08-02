using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace theInfrastructure
{
    public static class Helper
    {
        public static string ToHtmlValue(this InputType type)
        {
            return type switch
            {
                InputType.Text => "text",
                InputType.Password => "password",
                InputType.Email => "email",
                InputType.Number => "number",
                InputType.Integer => "number",
                InputType.Decimal => "number",
                InputType.Tel => "tel",
                InputType.Url => "url",
                InputType.Search => "search",
                InputType.Date => "date",
                InputType.Time => "time",
                InputType.DateTimeLocal => "datetime-local",
                InputType.Month => "month",
                InputType.Week => "week",
                InputType.Color => "color",
                InputType.Range => "range",
                InputType.Hidden => "hidden",
                _ => "text"
            };
        }
        public static string? GetStep(this InputType type)
        {
            return type switch
            {
                InputType.Integer => "1",
                InputType.Decimal => "any",
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


        public static object MapPropertiesByReflection(object UI, object DB)
        {
            // check Objects 
            if (UI == null)
                return null;

            if (DB == null)
                return null;

            // Check Type 
            Type _uiType = UI.GetType();
            Type _dbType = DB.GetType();
            if (_dbType.FullName != _uiType.FullName)
                return null;

            // Iterate over Properties 
            foreach (PropertyInfo pi in _uiType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                //check 
                if (pi == null)
                    continue;

                // not mapping GUID 
                string property = pi.Name;
                if (property == "GUID")
                    continue;

                if (property == "MasterGUID")
                    continue;

                // Check for NotMapped 
                NotMappedAttribute notM = (NotMappedAttribute)Attribute.GetCustomAttribute(pi, typeof(NotMappedAttribute));
                if (notM != null)
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
                    DB.GetType().GetProperty(property).SetValue(DB, uiValue);
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
