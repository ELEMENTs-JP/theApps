using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace theInfrastructure
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        static string filepath = "./Configuration/";

        public static void Save<T>(T instance, string filename) where T : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            try
            {
                string json = ToJsonString(instance);
                string crypto = Encryption.Encrypt(json);

                File.WriteAllText(filepath + filename, crypto);
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                throw;
            }
        }
        public static T Load<T>(string filename) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Dateipfad darf nicht leer sein.", nameof(filename));

            try
            {
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                if (!File.Exists(filepath + filename))
                {
                    T defaultInstance = new T();
                    Save(defaultInstance, filename);
                    return defaultInstance;
                }

                string crypto = File.ReadAllText(filepath + filename);
                string json = Encryption.Decrypt(crypto);

                T obj = ToObjectFromJson<T>(json);
                return obj ?? new T();
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return new T();
            }
        }

        private static string ToJsonString<T>(T theObject)
        {
            try
            {

                return JsonSerializer.Serialize(theObject, JsonOptions);
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return string.Empty;
            }
        }
        private static T ToObjectFromJson<T>(string json) where T : class, new()
        {
            if (string.IsNullOrEmpty(json))
            {
                return new T();
            }

            try
            {

                return JsonSerializer.Deserialize<T>(json, JsonOptions) ?? new T();
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return new T();
            }
        }
    }



}
