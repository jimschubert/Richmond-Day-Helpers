using System;
using System.IO;
using System.Security.Cryptography;

namespace RichmondDay.Helpers {
    /// <summary>
    /// This class is responsible for providing standard DES and
    /// triple DES encryption and decryption
    /// </summary>
    public class EncryptionUtility {
        //8 bytes randomly selected for both the Key and the Initialization Vector
        //the IV is used to encrypt the first block of text so that any repetitive 
        //patterns are not apparent
        private static byte[] KEY_64 = { 42, 16, 93, 156, 78, 4, 218, 32 };
        private static byte[] IV_64 = { 55, 103, 246, 79, 36, 99, 167, 3 };

        //24 byte or 192 bit key and IV for TripleDES
        private static byte[] KEY_192 = {42, 16, 93, 156, 78, 250, 218, 32, 15, 
										167, 44, 80, 26, 250, 155, 112, 
										2, 94, 11, 204, 119, 35, 184, 197};
        private static byte[] IV_192 = {55, 103, 246, 79, 36, 99, 167, 3, 42, 
									   250, 62, 83, 184, 7, 209, 13,	145, 
									   23, 200, 58, 173, 10, 121, 222};


        /// <summary>
        /// Standard DES encryption
        /// </summary>
        /// <param name="val">Accepts value to be encrypted using DES</param>
        /// <returns>Returns value encrypted in DES</returns>
        public static string Encrypt(string val) {
            string encrypted = "";
            if (val != "") {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(val);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //convert back to string - added explicit conversion to int32
                encrypted = Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            }
            return encrypted;
        }


        /// <summary>
        /// Standard DES decryption
        /// </summary>
        /// <param name="val">Value of decrypted</param>
        /// <returns>Returns decrypted value as string</returns>
        public static string Decrypt(string val) {
            string decrpted = "";
            if (val != "") {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                //convert from string to byte
                byte[] buffer = Convert.FromBase64String(val);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                decrpted = sr.ReadToEnd();
            }
            return decrpted;
        }


        /// <summary>
        /// Triple DES encryption
        /// </summary>
        /// <param name="val">Accepts value to be encrypted using Triple DES</param>
        /// <returns>Returns value encrypted in Triple DES</returns>
        public static string EncryptTripleDES(string val) {
            string encrypted = "";
            if (val != "") {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(val);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //convert back to string
                encrypted = Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            }
            return encrypted;
        }


        /// <summary>
        /// Triple DES decryption
        /// </summary>
        /// <param name="val">Value of decrypted</param>
        /// <returns>Returns decrypted value as string<</returns>
        public static string DecryptTripleDES(string val) {
            string decrtypted = "";
            if (val != "") {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();

                //convert from string to byte
                byte[] buffer = Convert.FromBase64String(val);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                decrtypted = sr.ReadToEnd();
            }
            return decrtypted;
        }
    }
}