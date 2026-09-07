using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        public static string IdentifyFileTypeWithByteArray(byte[] sourceFileBytes)
        {
            if (sourceFileBytes == null || sourceFileBytes.Length < 4)
                return "";

            // 1. MP3 mit ID3v2 Tag ("ID3")
            if (sourceFileBytes[0] == 0x49 && sourceFileBytes[1] == 0x44 && sourceFileBytes[2] == 0x33)
                return "mp3";

            // 2. MP3 ohne ID3 Tag (Frame Sync: 11 Einsen in Folge -> 0xFF gefolgt von 0xE0-0xFF)
            if (sourceFileBytes[0] == 0xFF && (sourceFileBytes[1] & 0xE0) == 0xE0)
                return "mp3";

            // 3. WAV / RIFF Container ("RIFF")
            if (sourceFileBytes[0] == 0x52 && sourceFileBytes[1] == 0x49 &&
                sourceFileBytes[2] == 0x46 && sourceFileBytes[3] == 0x46)
                return "wav";

            // 4. OGG Container ("OggS")
            if (sourceFileBytes[0] == 0x4F && sourceFileBytes[1] == 0x67 &&
                sourceFileBytes[2] == 0x67 && sourceFileBytes[3] == 0x53)
                return "ogg";

            // 5. FLAC ("fLaC")
            if (sourceFileBytes[0] == 0x66 && sourceFileBytes[1] == 0x4C &&
                sourceFileBytes[2] == 0x61 && sourceFileBytes[3] == 0x43)
                return "flac";

            // 6. WEBM / EBML Audio
            if (sourceFileBytes[0] == 0x1A && sourceFileBytes[1] == 0x45 &&
                sourceFileBytes[2] == 0xDF && sourceFileBytes[3] == 0xA3)
                return "weba";

            // 7. ISO Media Container (M4A / MP4) -> "ftyp" ab Index 4
            if (sourceFileBytes.Length >= 12 &&
                sourceFileBytes[4] == 0x66 && sourceFileBytes[5] == 0x74 &&
                sourceFileBytes[6] == 0x79 && sourceFileBytes[7] == 0x70)
            {
                // Prüfung auf M4A / M4B Subtypen
                string brand = System.Text.Encoding.ASCII.GetString(sourceFileBytes, 8, 4).ToLowerInvariant();
                if (brand.Contains("m4a") || brand.Contains("m4b") || brand.Contains("mp42"))
                    return "m4a";

                return "mp4";
            }

            // 8. Iteration für verbleibende Standard-Signaturen
            var fileSignatures = Helper.FileSignatures;
            foreach (var signature in fileSignatures)
            {
                if (sourceFileBytes.Length >= signature.Key.Length &&
                    sourceFileBytes.Take(signature.Key.Length).SequenceEqual(signature.Key))
                {
                    return signature.Value;
                }
            }

            return "";
        }
        public static string? GetExtensionFromMimeType(string mimeType) => mimeType?.ToLowerInvariant() switch
        {
            // Bilder
            "image/png" => "png",
            "image/jpeg" or "image/jpg" => "jpg",
            "image/gif" => "gif",
            "image/webp" => "webp",
            "image/bmp" => "bmp",
            "image/svg+xml" => "svg",

            // Audio & Video
            "audio/mpeg" or "audio/mp3" => "mp3",
            "audio/wav" or "audio/x-wav" => "wav",
            "audio/ogg" => "ogg",
            "video/mp4" => "mp4",
            "video/webm" => "webm",

            // Dokumente & Office
            "application/pdf" => "pdf",
            "application/msword" => "doc",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => "docx",
            "application/vnd.ms-excel" => "xls",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
            "application/vnd.ms-powerpoint" => "ppt",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation" => "pptx",
            "application/zip" => "zip",
            "text/plain" => "txt",
            _ => null
        };
        public static readonly List<KeyValuePair<byte[], string>> FileSignatures = new()
        {
            // --- AUDIO FORMATEN ---
            new(new byte[] { 0x49, 0x44, 0x33 }, "mp3"),
            new(new byte[] { 0xFF, 0xFB }, "mp3"),
            new(new byte[] { 0xFF, 0xF3 }, "mp3"),
            new(new byte[] { 0xFF, 0xF2 }, "mp3"),
            new(new byte[] { 0x52, 0x49, 0x46, 0x46 }, "wav"),
            new(new byte[] { 0x4F, 0x67, 0x67, 0x53 }, "ogg"),
            new(new byte[] { 0x66, 0x4C, 0x61, 0x43 }, "flac"),
            new(new byte[] { 0xFF, 0xF1 }, "aac"),
            new(new byte[] { 0xFF, 0xF9 }, "aac"),
            new(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, "weba"),
            new(new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 }, "wma"),
            new(new byte[] { 0x2E, 0x73, 0x6E, 0x64 }, "au"),
            new(new byte[] { 0x41, 0x49, 0x46, 0x46 }, "aiff"),

            // --- BILDER & DOKUMENTE ---
            new(new byte[] { 0xFF, 0xD8, 0xFF }, "jpg"),
            new(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, "png"),
            new(new byte[] { 0x47, 0x49, 0x46, 0x38 }, "gif"),
            new(new byte[] { 0x25, 0x50, 0x44, 0x46 }, "pdf"),
            new(new byte[] { 0x42, 0x4D }, "bmp"),
            new(new byte[] { 0x49, 0x49, 0x2A, 0x00 }, "tiff"),
            new(new byte[] { 0x4D, 0x4D, 0x00, 0x2A }, "tiff"),
            new(new byte[] { 0x52, 0x49, 0x46, 0x46, 0x57, 0x45, 0x42, 0x50 }, "webp"),
            new(new byte[] { 0x00, 0x00, 0x01, 0x00 }, "ico"),
            new(new byte[] { 0x49, 0x49, 0xBC }, "jxr"),
            new(new byte[] { 0x0A, 0x05, 0x01, 0x08 }, "pcx"),
            new(new byte[] { 0x38, 0x42, 0x50, 0x53 }, "psd"),
            new(new byte[] { 0x25, 0x21, 0x50, 0x53 }, "ai"),
            new(new byte[] { 0x46, 0x57, 0x53 }, "swf"),
            new(new byte[] { 0x43, 0x57, 0x53 }, "swf"),
            new(new byte[] { 0x06, 0x06, 0xED, 0xF5, 0xD8, 0x1D, 0x46, 0xE5, 0xBD, 0x31, 0xEF, 0xE7, 0xFE, 0x74, 0xB7, 0x1D }, "indd"),

            // --- ARCHIVE & OFFICE ---
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04 }, "zip"),
            new(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }, "doc"),
            new(new byte[] { 0x1F, 0x8B }, "gz"),
            new(new byte[] { 0x78, 0x9C }, "zlib"),
            new(new byte[] { 0x00, 0x01, 0x42, 0x44 }, "fla"),
            new(new byte[] { 0xEC, 0xA5, 0xC1, 0x00 }, "doc"),
            new(new byte[] { 0x0D, 0x44, 0x4F, 0x43 }, "doc")
        };

        public static async Task<byte[]> GetFileHeaderByUrlAsync(string url)
        {
            using var client = new HttpClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                // Request von 512 Bytes stellt sicher, dass MP3/M4A/RIFF-Header vollständig enthalten sind
                request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 511);

                using var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return Array.Empty<byte>();

                await using var stream = await response.Content.ReadAsStreamAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        public static byte[] GetFileHeader(string filePath, int headerSize = 8)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] headerBytes = new byte[headerSize];
                int bytesRead = fs.Read(headerBytes, 0, headerSize);

                // Bei Dateien kleiner als HeaderSize füllen wir den Rest mit Nullen
                if (bytesRead < headerSize)
                {
                    Array.Resize(ref headerBytes, bytesRead);
                }
                return headerBytes;
            }
        }

        public static long MaxFileSize(int defaultValue = 100)
        {
            long defaultFileSizeInBytes = 1024 * 1024 * defaultValue;
            return defaultFileSizeInBytes;
        }
        public static string GetAudioMimeType(this string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return "application/octet-stream";

            ReadOnlySpan<char> span = ext.AsSpan().TrimStart('.');

            if (span.Equals("mp3", StringComparison.OrdinalIgnoreCase))
                return "audio/mpeg";

            if (span.Equals("wav", StringComparison.OrdinalIgnoreCase))
                return "audio/wav";

            if (span.Equals("ogg", StringComparison.OrdinalIgnoreCase) || span.Equals("oga", StringComparison.OrdinalIgnoreCase))
                return "audio/ogg";

            if (span.Equals("m4a", StringComparison.OrdinalIgnoreCase) || span.Equals("aac", StringComparison.OrdinalIgnoreCase))
                return "audio/mp4";

            if (span.Equals("flac", StringComparison.OrdinalIgnoreCase))
                return "audio/flac";

            if (span.Equals("weba", StringComparison.OrdinalIgnoreCase))
                return "audio/webm";

            if (span.Equals("opus", StringComparison.OrdinalIgnoreCase))
                return "audio/ogg"; // Alternativ "audio/opus" gemäß RFC 7845

            return "application/octet-stream";
        }
        public static bool IsValidAudioExtension(this string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return false;

            ReadOnlySpan<char> span = ext.AsSpan().TrimStart('.');

            return span.Equals("mp3", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("wav", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("ogg", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("oga", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("m4a", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("aac", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("flac", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("weba", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("opus", StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsValidImageExtension(this string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return false;

            ReadOnlySpan<char> span = ext.AsSpan().TrimStart('.');

            return span.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("png", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("webp", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("svg", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("ico", StringComparison.OrdinalIgnoreCase) ||
                   span.Equals("avif", StringComparison.OrdinalIgnoreCase);
        }
        public static async Task<string> ToBase64String(IDTO image, bool loadSmallifAvailable = false)
        {
            string Base64String = string.Empty;
            if (image == null)
                return Base64String;

            string FilePath = image["FullFilePath"].ToSecureString();
            string ext = image["FileExtension"].ToSecureString();

            if (System.IO.File.Exists(FilePath))
            {
                if (loadSmallifAvailable)
                {
                    // Pfad für die _min-Datei ermitteln
                    string directory = System.IO.Path.GetDirectoryName(FilePath) ?? string.Empty;
                    string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(FilePath);
                    string fileExt = System.IO.Path.GetExtension(FilePath);

                    string minFilePath = System.IO.Path.Combine(directory, $"{fileNameWithoutExt}_min{fileExt}");

                    // Falls die _min-Datei existiert, wird deren Pfad verwendet
                    if (System.IO.File.Exists(minFilePath))
                    {
                        FilePath = minFilePath;
                    }
                }

                // Read 
                byte[] arr = System.IO.File.ReadAllBytes(FilePath);

                string base64String = Convert.ToBase64String(arr, 0, arr.Length);

                Base64String = "data:" + CorrectBase64Extension(ext) + ";base64," + base64String;
            }

            // src="data:application/pdf;base64,@Base64String" 
            return Base64String;

        }
        public static byte[] GetFileAsByteArray(this IDTO file)
        {
            try
            {
                if (file != null)
                {
                    string FilePath = file["FullFilePath"].ToSecureString();
                    string ext = file["FileExtension"].ToSecureString();

                    if (System.IO.File.Exists(FilePath))
                    {
                        // Read
                        byte[] arr = System.IO.File.ReadAllBytes(FilePath);
                        return arr;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("FAIL: " + ex.Message);
            }

            var randomBinaryData = new byte[1 * 1024];
            return randomBinaryData;
        }
        public static string CorrectBase64Extension(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
            {
                return "application/octet-stream";
            }

            // Führenden Punkt entfernen (falls vorhanden) und in Kleinbuchstaben umwandeln
            string cleanExt = ext.TrimStart('.').ToLowerInvariant();

            // Mappings für gängige Web-Dateiformate
            return cleanExt switch
            {
                // Dokumente
                "pdf" => "application/pdf",
                "txt" => "text/plain",
                "csv" => "text/csv",
                "json" => "application/json",
                "xml" => "application/xml",

                // Bilder (Standard & Vektor)
                "png" => "image/png",
                "jpg" or "jpeg" => "image/jpeg",
                "gif" => "image/gif",
                "webp" => "image/webp",
                "svg" => "image/svg+xml",
                "bmp" => "image/bmp",
                "ico" => "image/x-icon",
                "tiff" or "tif" => "image/tiff",

                // Audio
                "mp3" => "audio/mpeg",
                "wav" => "audio/wav",
                "ogg" => "audio/ogg",
                "aac" => "audio/aac",
                "m4a" => "audio/mp4",

                // Video
                "mp4" => "video/mp4",
                "webm" => "video/webm",
                "ogv" => "video/ogg",

                // Standard Fallback für binäre Daten
                _ => "application/octet-stream"
            };
        }

        //public static string CorrectBase64Extension(string ext)
        //{
        //    if (ext == "pdf")
        //    {
        //        return "application/pdf";
        //    }

        //    return ext;
        //}

    }
}
