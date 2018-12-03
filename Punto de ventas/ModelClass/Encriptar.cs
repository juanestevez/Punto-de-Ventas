using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Punto_de_ventas.ModelClass
{
    public class Encriptar
    {
        private static RijndaelManaged rm = new RijndaelManaged();

        public Encriptar()
        {
            // Modo de funcionamiento
            rm.Mode = CipherMode.CBC;
            // Mode de relleno
            rm.Padding = PaddingMode.PKCS7;
            // Tamaño de la clave secreta
            rm.KeySize = 0x80;
            // Tamaño del bloqueo
            rm.BlockSize = 0x80;
        }

        public static string EncriptarDatos(string texto, string clave)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(clave);
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            rm.Key = EncryptionkeyBytes;
            rm.IV = EncryptionkeyBytes;
            ICryptoTransform objtransform = rm.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(texto);

            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public static string DesencriptarDatos(string textoEncriptado, string claveEncriptacion)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(textoEncriptado);
            byte[] passBytes = Encoding.UTF8.GetBytes(claveEncriptacion);
            byte[] encryptionKeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > encryptionKeyBytes.Length)
            {
                len = encryptionKeyBytes.Length;
            }
            Array.Copy(passBytes, encryptionKeyBytes, len);
            rm.Key = encryptionKeyBytes;
            rm.IV = encryptionKeyBytes;
            byte[] textByte = rm.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(textByte);
        }
    }
}
