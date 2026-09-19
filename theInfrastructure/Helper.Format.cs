using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        public static string ToValueIfNullOrEmpty(this object? obj, string value)
        {
            if (obj == null)
            {
                return value;
            }

            string str = obj.ToSecureString();
            return string.IsNullOrEmpty(str) ? value : str;
        }
        public static bool IsEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsValidHttpsUrl(this string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            ReadOnlySpan<char> span = url.AsSpan().Trim();

            // 1. Schema-Prüfung ("https://")
            if (!span.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Schema abschneiden, um den Host-Teil zu analysieren
            span = span.Slice(8);

            // 2. Pfad/Query/Fragment abspalten, falls vorhanden
            int pathIndex = span.IndexOfAny('/', '?', '#');
            if (pathIndex >= 0)
            {
                span = span.Slice(0, pathIndex);
            }

            // 3. Port abspalten, falls vorhanden
            int portIndex = span.LastIndexOf(':');
            if (portIndex >= 0)
            {
                // Prüfen, ob nach dem Doppelpunkt ein gültiger Port folgt
                ReadOnlySpan<char> portSpan = span.Slice(portIndex + 1);
                if (portSpan.IsEmpty || !ushort.TryParse(portSpan, out _))
                {
                    return false;
                }
                span = span.Slice(0, portIndex);
            }

            // Host-Teil darf nach Entfernen von Port/Pfad nicht leer sein
            if (span.IsEmpty)
            {
                return false;
            }

            // 4. Prüfen auf gültigen Host (IPv4 oder Domain mit TLD)
            // Kein Punkt am Anfang oder Ende
            if (span[0] == '.' || span[^1] == '.')
            {
                return false;
            }

            // Host muss mindestens einen Punkt enthalten (z. B. "domain.com")
            // Ausgenommen: "localhost" für lokale Tests
            int dotIndex = span.IndexOf('.');
            if (dotIndex <= 0)
            {
                return span.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            }

            // Keine zwei Punkte aufeinanderfolgend ("..")
            for (int i = 0; i < span.Length - 1; i++)
            {
                if (span[i] == '.' && span[i + 1] == '.')
                {
                    return false;
                }
            }

            return true;
        }

        public static List<IDTO> DefaultColors()
        {
            List<IDTO> Items = new();

        
            Items.Add(new DTO() { ID = "#00000000", Title = "transparent" });
            Items.Add(new DTO() { ID = "#ffffff", Title = "weiß" });
            Items.Add(new DTO() { ID = "#a1a5ab", Title = "hellgrau" });
            Items.Add(new DTO() { ID = "#626976", Title = "grau" });
            Items.Add(new DTO() { ID = "#444a55", Title = "dunkelgrau" });
            Items.Add(new DTO() { ID = "#182433", Title = "schwarz" });
            Items.Add(new DTO() { ID = "#8914a5", Title = "violett" });
            Items.Add(new DTO() { ID = "#c11515", Title = "rot" });
            Items.Add(new DTO() { ID = "#f76707", Title = "orange" });
            Items.Add(new DTO() { ID = "#f59f00", Title = "gelb" });
            Items.Add(new DTO() { ID = "#1d9d31", Title = "grün" });
            Items.Add(new DTO() { ID = "#066fd1", Title = "blau" });
            Items.Add(new DTO() { ID = "#084a87", Title = "marine" });

            return Items;
        }


        // Colors 
        private static readonly string[] Formats = ["D", "N", "B", "P", "X"];

        public static bool IsValidGuid(this string? input)
        {
            Guid parsedGuid = Guid.Empty;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            ReadOnlySpan<char> span = input.AsSpan().Trim();

            // Prüft strikt gegen die vorgegebenen GUID-Formate (verhindert False Positives)
            foreach (var format in Formats)
            {
                if (Guid.TryParseExact(span, format, out parsedGuid))
                {
                    return true;
                }
            }

            return false;
        }
        public static List<tspColor> GetAllColors()
        {
            List<tspColor> colors = new List<tspColor>();

            colors.Add(new tspColor { HEX = "#1BA031", Color = GeneralColor.GreenDark });
            colors.Add(new tspColor { HEX = "#34A853", Color = GeneralColor.Green });
            colors.Add(new tspColor { HEX = "#36DB3C", Color = GeneralColor.GreenLight });

            colors.Add(new tspColor { HEX = "#D3133F", Color = GeneralColor.RedDark });
            colors.Add(new tspColor { HEX = "#E2104A", Color = GeneralColor.Red });
            colors.Add(new tspColor { HEX = "#EA2765", Color = GeneralColor.RedLight });

            colors.Add(new tspColor { HEX = "#0067CE", Color = GeneralColor.BlueDark });
            colors.Add(new tspColor { HEX = "#008CD8", Color = GeneralColor.Blue });
            colors.Add(new tspColor { HEX = "#0B91E5", Color = GeneralColor.BlueLight });

            colors.Add(new tspColor { HEX = "#AB117F", Color = GeneralColor.PinkLight });
            colors.Add(new tspColor { HEX = "#AB117F", Color = GeneralColor.Pink });
            colors.Add(new tspColor { HEX = "#AB117F", Color = GeneralColor.PinkDark });


            colors.Add(new tspColor { HEX = "#3B2F77", Color = GeneralColor.ViolettLight });
            colors.Add(new tspColor { HEX = "#3B2F77", Color = GeneralColor.Violett });
            colors.Add(new tspColor { HEX = "#3B2F77", Color = GeneralColor.ViolettDark });


            colors.Add(new tspColor { HEX = "#FCC200", Color = GeneralColor.YellowLight });
            colors.Add(new tspColor { HEX = "#FCC200", Color = GeneralColor.Yellow });
            colors.Add(new tspColor { HEX = "#FCC200", Color = GeneralColor.YellowDark });

            colors.Add(new tspColor { HEX = "#00A9A0", Color = GeneralColor.TurquoiseLight });
            colors.Add(new tspColor { HEX = "#00A9A0", Color = GeneralColor.Turquoise });
            colors.Add(new tspColor { HEX = "#00A9A0", Color = GeneralColor.TurquoiseDark });

            colors.Add(new tspColor { HEX = "#E2007D", Color = GeneralColor.RoseLight });
            colors.Add(new tspColor { HEX = "#E2007D", Color = GeneralColor.Rose });
            colors.Add(new tspColor { HEX = "#E2007D", Color = GeneralColor.RoseDark });

            colors.Add(new tspColor { HEX = "#E87A2C", Color = GeneralColor.OrangeLight });
            colors.Add(new tspColor { HEX = "#E87A2C", Color = GeneralColor.Orange });
            colors.Add(new tspColor { HEX = "#E87A2C", Color = GeneralColor.OrangeDark });

            // colors.Add(new tspColor { HEX = "#DC0A15", Color = GeneralColor.SignalRot });

            colors.Add(new tspColor { HEX = "#dddddd", Color = GeneralColor.Light });
            colors.Add(new tspColor { HEX = "#cccccc", Color = GeneralColor.Silver });
            colors.Add(new tspColor { HEX = "#aaaaaa", Color = GeneralColor.Grey });
            colors.Add(new tspColor { HEX = "#888888", Color = GeneralColor.Gray30 });
            colors.Add(new tspColor { HEX = "#666666", Color = GeneralColor.Gray50 });
            colors.Add(new tspColor { HEX = "#444444", Color = GeneralColor.Gray70 });
            colors.Add(new tspColor { HEX = "#222222", Color = GeneralColor.Dark });
            colors.Add(new tspColor { HEX = "#111111", Color = GeneralColor.Deep });
            colors.Add(new tspColor { HEX = "#000000", Color = GeneralColor.Deep });

            return colors;
        }
        public static tspColor GetColor(GeneralColor color)
        {
            return GetAllColors().FirstOrDefault(se => se.Color == color);
        }
        public static string ToNormalizedString(this string text, string toRemoveWord = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            // 1. Wort entfernen (falls angegeben)
            if (!string.IsNullOrEmpty(toRemoveWord))
            {
                text = text.Replace(toRemoveWord, string.Empty);
            }

            if (text.Length == 0)
            {
                return string.Empty;
            }

            // 2. Maximale Kapazität vorab berechnen, um Array-Resizing zu verhindern
            // Im Worst-Case (nur Großbuchstaben) verdoppelt sich die Länge.
            StringBuilder newString = new StringBuilder(text.Length * 2);

            ReadOnlySpan<char> span = text.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                char c = span[i];

                // Leerzeichen nur VOR Großbuchstaben einfügen, aber NICHT an Index 0
                if (char.IsUpper(c) && i > 0 && newString.Length > 0 && newString[newString.Length - 1] != ' ')
                {
                    newString.Append(' ');
                }

                newString.Append(c);
            }

            return newString.ToString();
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
        public static string ToDateField(this object? obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            // 1. Echter DateTime-Typ
            if (obj is DateTime dt)
            {
                return dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            // 2. TimeSpan-Typ (Interpretation als Dauer seit/ab einem Basisdatum oder Zeitanteil)
            if (obj is TimeSpan ts)
            {
                return DateTime.MinValue.Add(ts).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            // 3. String-Eingaben
            string strInput = obj.ToString()?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return string.Empty;
            }

            // Versuch, den String in ein gültiges Datum zu parsen (unterstützt verschiedene Formate & Kulturen)
            if (DateTime.TryParse(strInput, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) ||
                DateTime.TryParse(strInput, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            // Rückgabe bei nicht parsbaren Werten
            return string.Empty;
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

        public static DateTime TrimToMinute(this DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                0,
                0,
                dateTime.Kind);
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

        private static readonly CultureInfo GermanCulture = CultureInfo.GetCultureInfo("de-DE");
        public static string ToFormat(this string input, TextFormat format)
        {
            switch (format)
            {
                case TextFormat.Datum:
                    {
                        if (string.IsNullOrEmpty(input))
                            return string.Empty;

                        CultureInfo culture = new CultureInfo("de-DE");
                        return input.ToSecureDateTime().ToString("ddd, d. MMM yyyy", culture);
                    }
                case TextFormat.Text:
                    {
                        return input.ToSecureString();
                    }
                case TextFormat.Integer:
                    {
                        return input.ToSecureInt().ToSecureString();
                    }
                case TextFormat.Decimal:
                    {
                        return input.ToSecureDecimal().ToString("F2");
                    }
                case TextFormat.Byte:
                    {
                        return input.ToDecimalFormat() + " Byte";
                    }
                case TextFormat.KB:
                    {
                        return input.ToDecimalFormat() + " KB";
                    }
                case TextFormat.MB:
                    {
                        return input.ToDecimalFormat() + " MB";
                    }
                default:
                    {
                        break;
                    }
            }

            return input;
        }
        public static string ToDecimalFormat(this string input, int maxDecimals = 2)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Parsing der deutschen Zahlenformatierung
            if (decimal.TryParse(input, NumberStyles.Number, GermanCulture, out decimal parsedValue))
            {
                // "0.##" formatiert auf maximal 2 Nachkommastellen ohne auffüllende Nullen am Ende.
                // Für exakt 2 Nachkommastellen (z. B. "1.234,50") stattdessen "N2" verwenden.
                string formatPattern = "0." + new string('#', maxDecimals);
                return parsedValue.ToString(formatPattern, GermanCulture);
            }

            return input; // Rückgabe des Originalstrings, falls das Parsing fehlschlägt
        }
        public static int WeekOfYear(this DateTime date)
        {
            // Verwende die Kulturinformationen, um die Kalenderwoche zu berechnen
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek; // ISO-8601 Konvention
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday; // ISO-8601: Woche beginnt am Montag

            // Berechne die Kalenderwoche und gib sie zurück
            return calendar.GetWeekOfYear(date, weekRule, firstDayOfWeek);
        }
        public static string GetItemUrl(IDTO dto)
        {
            // Url 
            return "/Item/" + dto.ItemType.ToSecureString() + "/" + dto.GUID.ToSecureString();
        }
        public static int GetDayOfWeek(DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Monday:
                    { return 1; }
                case DayOfWeek.Tuesday:
                    { return 2; }
                case DayOfWeek.Wednesday:
                    { return 3; }
                case DayOfWeek.Thursday:
                    { return 4; }
                case DayOfWeek.Friday:
                    { return 5; }
                case DayOfWeek.Saturday:
                    { return 6; }
                case DayOfWeek.Sunday:
                    { return 7; }
            }

            return 0;
        }
        public static List<DateTime> GetDates(int year, int month)
        {
            List<DateTime> dates = Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list


            // Vorher
            DateTime first = dates.FirstOrDefault();
            int before = GetDayOfWeek(first.DayOfWeek) - 1;
            for (int b = 0; b < before; b++)
            {
                dates.Insert(0, first.AddDays(-b - 1));
            }

            // Nachher
            DateTime last = dates.LastOrDefault();
            int after = 7 - GetDayOfWeek(last.DayOfWeek);
            for (int a = 0; a < after; a++)
            {
                dates.Add(last.AddDays(a + 1));
            }

            // return
            return dates;
        }
        public static string ToYearMonth(this DateTime date, string trennzeichen = "")
        {
            return date.Date.Year + trennzeichen + date.Date.Month;
        }

    }
}
