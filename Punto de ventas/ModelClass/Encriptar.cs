using System;
using System.Text;
using System.Security.Cryptography;

namespace Punto_de_ventas.ModelClass
{
    public class Encriptar
    {
        private static RijndaelManaged rm = new RijndaelManaged();

        public Encriptar()
        {
            // establece el modo para el funcionamiento del algoritmo
            rm.Mode = CipherMode.CBC;
            // establece el modo de relleno utilizado en el algoritmo.
            rm.Padding = PaddingMode.PKCS7;
            // establece el tamaño, en bits, para la clave secreta.
            rm.KeySize = 0x80;
            // establece el tamaño del bloque en bits para la operación criptográfica.
            rm.BlockSize = 0x80;
        }

        public static string EncryptData(string textData, string Encryptionkey)
        {
            byte[]
                passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            // establece el vector de inicialización (IV) para el algoritmo simétrico   
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            rm.Key = EncryptionkeyBytes;
            rm.IV = EncryptionkeyBytes;
            // Crea un objeto AES simétrico con la clave actual y el vector de inicialización IV.
            ICryptoTransform objtransform = rm.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public static string DecryptData(string EncryptedText, string Encryptionkey)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            rm.Key = EncryptionkeyBytes;
            rm.IV = EncryptionkeyBytes;
            byte[] TextByte = rm.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);
        }
    }
}
