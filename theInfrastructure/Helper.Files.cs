using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {
        public static long MaxFileSize(int defaultValue = 10)
        {
            long defaultFileSizeInBytes = 1024 * 1024 * defaultValue;
            return defaultFileSizeInBytes;
        }
        public static bool IsValidImageExtension(string ext)
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
        public static async Task<string> ToBase64String(IDTO image)
        {
            string Base64String = string.Empty;
            if (image == null)
                return Base64String;

            string FilePath = image["FullFilePath"].ToSecureString();
            string ext = image["FileExtension"].ToSecureString();

            if (System.IO.File.Exists(FilePath))
            {
                // Read
                byte[] arr = System.IO.File.ReadAllBytes(FilePath);

                string base64String = Convert.ToBase64String(arr, 0, arr.Length);

                Base64String = "data:" + ext + ";base64," + base64String;
            }

            return Base64String;

        }

    }
}
