using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;



public class MasterKeyService
{
    private readonly string _keyFilePath;

    public MasterKeyService(IWebHostEnvironment env)
    {
        _keyFilePath = Path.Combine(env.ContentRootPath, "Configuration", "master.key");
    }

    private string CreateAndProtectNewKey()
    {
        byte[] rawKeyBytes = RandomNumberGenerator.GetBytes(32);
        byte[] encryptedBytes = ProtectedData.Protect(rawKeyBytes, null, DataProtectionScope.LocalMachine);

        string directoryPath = Path.GetDirectoryName(_keyFilePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllBytes(_keyFilePath, encryptedBytes);

        return Convert.ToBase64String(rawKeyBytes);
    }

    public string MasterKey()
    {
        if (!File.Exists(_keyFilePath))
        {
            return CreateAndProtectNewKey();
        }

        try
        {
            byte[] encryptedBytes = File.ReadAllBytes(_keyFilePath);
            byte[] rawKeyBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.LocalMachine);

            return Convert.ToBase64String(rawKeyBytes);
        }
        catch (Exception ex)
        {
            return $"Entschlüsselungsfehler: {ex.Message}";
        }
    }
}