using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KONTAKTOR.DA.Infrastructure.Security
{
    /// <summary>
    /// Инкапсулирует логику шифрования/дешифрования (используется для ConnectionString)
    /// </summary>
    public static class Encryptor
    {
        /// <summary>
        /// Кодирование строки по ключу методом SHA-256 (используем встроенные классы)
        /// </summary>
        /// <param name="source">Текст для шифрования</param>
        /// <param name="key">Ключ шифрования</param>
        /// <param name="salt">Ключевая привязка (соль)</param>
        /// <param name="alg">Имя хэш-алгоритма</param>
        /// <param name="iter">Число итераций для формирования ключа</param>
        /// <param name="initVec">Вектор инициализации</param>
        /// <param name="keySize">Размер ключа по умолчанию</param>
        /// <returns>Зашированная строка в Base64</returns>
        public static string Encrypt(string source, string key = "Y2VudHJvcw==", string salt = "centros", string alg = "SHA256", int iter = 2, string initVec = "a8doSuDit0z1hZe#", int keySize = 256)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            // Кодируем все в байты
            byte[] initVecBytes = Encoding.ASCII.GetBytes(initVec);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] textBytes = Encoding.ASCII.GetBytes(source);

            var deriveBytes = new PasswordDeriveBytes(key, saltBytes, alg, iter);
            byte[] keyBytes = deriveBytes.GetBytes(keySize / 8); // Переводим в байты размер ключа
            var symmK = new RijndaelManaged() { Mode = CipherMode.CBC }; // Алгоритм для ключа

            byte[] cipherTextBytes = null;

            // Объект-шифратор для алгоритма симметричного шифрования с заданным ключом
            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecBytes))
            {
                // Поток, резервным хранилищем которого является память
                using (var memStream = new MemoryStream())
                {
                    // Определяем поток, связывающий поток данных с криптографическими преобразованиями
                    using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(textBytes, 0, textBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes); // Строковое представление в Base64
        }

        /// <summary>
        /// Декодирование строки по ключу методом SHA-256 (используем встроенные классы)
        /// </summary>
        /// <param name="encoded">Текст для дешифрования (Base64)</param>
        /// <param name="key">Ключ дешифрования</param>
        /// <param name="salt">Ключевая привязка (соль)</param>
        /// <param name="alg">Имя хэш-алгоритма</param>
        /// <param name="iter">Число итераций для формирования ключа</param>
        /// <param name="initVec">Вектор инициализации</param>
        /// <param name="keySize">Размер ключа по умолчанию</param>
        /// <returns>Расшированная строка в UTF-8</returns>
        public static string Decrypt(string encoded, string key = "Y2VudHJvcw==", string salt = "centros", string alg = "SHA256", int iter = 2, string initVec = "a8doSuDit0z1hZe#", int keySize = 256)
        {
            if (string.IsNullOrEmpty(encoded))
            {
                return string.Empty;
            }

            // Кодируем все символы в байты
            byte[] initVecBytes = Encoding.ASCII.GetBytes(initVec);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] textBytes = Convert.FromBase64String(encoded);

            var deriveBytes = new PasswordDeriveBytes(key, saltBytes, alg, iter);
            byte[] keyBytes = deriveBytes.GetBytes(keySize / 8); // Переводим в байты размер ключа
            var symmK = new RijndaelManaged() { Mode = CipherMode.CBC }; // Алгоритм для ключа

            byte[] plainTextBytes = new byte[textBytes.Length];
            int byteCount = 0;

            // Объект-дешифратор для алгоритма симметричного дешифрования с заданным ключом
            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecBytes))
            {
                // Поток, резервным хранилищем которого является память
                using (var memStream = new MemoryStream(textBytes))
                {
                    // Определяем поток, связывающий поток данных с криптографическими преобразованиями
                    using (var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    }
                }
            }

            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount); // Строковое представление в UTF-8
        }
    }
}
