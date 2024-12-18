﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace General
{
    public static class AesCryptoService
    {
        private const int AesKeySize = 16;

        public static string AesEncrypt(string data, string key)
        {
            return AesEncrypt(data, Encoding.UTF8.GetBytes(key));
        }

        public static string AesDecrypt(string data, string key)
        {
            return AesDecrypt(data, Encoding.UTF8.GetBytes(key));
        }

        private static string AesEncrypt(string data, byte[] key)
        {
            return Convert.ToBase64String(AesEncrypt(Encoding.UTF8.GetBytes(data), key));
        }

        private static string AesDecrypt(string data, byte[] key)
        {
            return Encoding.UTF8.GetString(AesDecrypt(Convert.FromBase64String(data), key));
        }

        private static byte[] AesEncrypt(byte[] data, byte[] key)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }

            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} cannot be empty");
            }

            if (key.Length != AesKeySize)
            {
                Array.Resize(ref key, AesKeySize);
            }

            using (var aes = new AesCryptoServiceProvider
                   {
                       Key = key,
                       Mode = CipherMode.CBC,
                       Padding = PaddingMode.PKCS7
                   })
            {
                aes.GenerateIV();
                var iv = aes.IV;
                using (var encrypter = aes.CreateEncryptor(aes.Key, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var tCryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var tBinaryWriter = new BinaryWriter(tCryptoStream))
                    {
                        // prepend IV to data
                        cipherStream.Write(iv, 0, iv.Length);
                        tBinaryWriter.Write(data);
                        tCryptoStream.FlushFinalBlock();
                    }

                    var cipherBytes = cipherStream.ToArray();

                    return cipherBytes;
                }
            }
        }

        private static byte[] AesDecrypt(byte[] data, byte[] key)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }

            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} cannot be empty");
            }

            if (key.Length != AesKeySize)
            {
                Array.Resize(ref key, AesKeySize);
            }

            using (var aes = new AesCryptoServiceProvider
                   {
                       Key = key,
                       Mode = CipherMode.CBC,
                       Padding = PaddingMode.PKCS7
                   })
            {
                // get first KeySize bytes of IV and use it to decrypt
                var iv = new byte[AesKeySize];
                Array.Copy(data, 0, iv, 0, iv.Length);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cs))
                    {
                        // decrypt cipher text from data, starting just past the IV
                        binaryWriter.Write(
                            data,
                            iv.Length,
                            data.Length - iv.Length
                        );
                    }

                    var dataBytes = ms.ToArray();

                    return dataBytes;
                }
            }
        }
    }
}