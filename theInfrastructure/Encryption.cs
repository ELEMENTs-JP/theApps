using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace theInfrastructure
{
    public class Encryption
    {
        static string Password = "UseOrInjectSecurePassword123!hereAndFindaBetterSolution";
        static string Salt = "UseOrInjectSecureHash321#alsoHereAndFindaBetterSolution";

        public static string Encrypt(string clearText)
        {
            // Verschlüsselung 
            try
            {
                // TODO: Remove Password 
                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

                // TODO: Remove Salt 
                byte[] saldBytes = System.Text.Encoding.Unicode.GetBytes(Salt);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, saldBytes);
                byte[] encryptedData = EncryptString(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception ex)
            {
                return clearText;
            }
        }
        private static byte[] EncryptString(byte[] clearText, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Aes alg = Aes.Create();
                alg.Key = Key;
                alg.IV = IV;
                CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearText, 0, clearText.Length);
                cs.Close();
                byte[] encryptedData = ms.ToArray();
                return encryptedData;
            }
            catch (Exception ex)
            {
                return clearText;
            }
        }
        public static string Decrypt(string cipherText)
        {
            // Entschlüsselung 
            try
            {
                // TODO: Remove Password 
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // TODO: Remove Salt 
                byte[] saldBytes = System.Text.Encoding.Unicode.GetBytes(Salt);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, saldBytes);
                byte[] decryptedData = DecryptString(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

                string returnText = System.Text.Encoding.Unicode.GetString(decryptedData);

                //if (IsEncryptedString(returnText) == true)
                //{
                //    return cipherText;
                //}
                if (CheckStringForAscii(returnText) == false)
                {
                    if (CheckStringForAscii(cipherText) == true)
                    {
                        return cipherText;
                    }
                }

                return returnText;
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }
        private static byte[] DecryptString(byte[] cipherData, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Aes alg = Aes.Create();
                alg.Key = Key;
                alg.IV = IV;
                CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
                byte[] decryptedData = ms.ToArray();
                return decryptedData;
            }
            catch (Exception ex)
            {
                return cipherData;
            }
        }
        private static bool IsEncryptedString(string text)
        {
            // Encrypted 
            if (text.Contains("=") ||
                text.Contains("+"))
                return true;

            // clean 
            return false;
        }
        private static bool CheckStringForAscii(string text)
        {
            // bool isAscii = c < 128; 
            foreach (char c in text.ToCharArray())
            {
                if (c >= 256)
                    return false;
            }
            return true;
        }
    }
}
