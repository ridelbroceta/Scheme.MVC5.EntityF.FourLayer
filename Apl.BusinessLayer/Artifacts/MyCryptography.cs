using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Apl.BusinessLayer.Artifacts
{
    class MyCryptography
    {
        private static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string EncryptPass(string input)
        {
            return ComputeHash(input, new SHA256CryptoServiceProvider());
        }

        private static readonly byte[] Clave = Encoding.ASCII.GetBytes("+10KKcbpV8PIeSu7");
        private static readonly byte[] Iv = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        public static string Encrypt(string cadena)
        {

            byte[] inputBytes = Encoding.ASCII.GetBytes(cadena);
            byte[] encripted;
            var cripto = new RijndaelManaged();
            using (var ms = new MemoryStream(inputBytes.Length))
            {
                using (var objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, Iv), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public static string UnEncrypt(string cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(cadena);
            string textoLimpio;
            var cripto = new RijndaelManaged();
            using (var ms = new MemoryStream(inputBytes))
            {
                using (var objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, Iv), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }

    }
}
