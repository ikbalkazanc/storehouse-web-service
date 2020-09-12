using System;
using System.Security.Cryptography;
using System.Text;

namespace KeyGenerator
{
    public class KeyGenerator
    {
        private static string keyCharacters = @"qwertyuopasdfghjklizxcvbnm1234567890";
        private static string idCharacters = @"1234567890";
        private static string qrCharacters = @"QWERTYUIOPLKJHGFDSAZXCVBNM1234567890123456789123456789";
        private static int max_size_key = 50;
        private static int max_size_id = 6;
        private static int max_size_request = 8;
        private static int max_size_qr = 7;
        private static RNGCryptoServiceProvider KeyGenCore = new RNGCryptoServiceProvider();

        public static string Generate()
        {
            char[] charactors = new char[keyCharacters.Length];
            charactors = keyCharacters.ToCharArray();
            byte[] data = new byte[1];
           
            KeyGenCore.GetNonZeroBytes(data);
            data = new byte[max_size_key];
            KeyGenCore.GetNonZeroBytes(data);
            
            StringBuilder ResultKey = new StringBuilder(max_size_key);
            foreach (byte b in data)
            {
                ResultKey.Append(charactors[b % (charactors.Length)]);
            }
            return ResultKey.ToString();
        }
        public static string GenerateDecimal()
        {
            char[] charactors = new char[idCharacters.Length];
            charactors = idCharacters.ToCharArray();
            byte[] data = new byte[1];

            KeyGenCore.GetNonZeroBytes(data);
            data = new byte[max_size_id];
            KeyGenCore.GetNonZeroBytes(data);

            StringBuilder ResultKey = new StringBuilder(max_size_id);
            foreach (byte b in data)
            {
                ResultKey.Append(charactors[b % (charactors.Length)]);
            }
            return ResultKey.ToString();
        }
        public static string GenerateRequestId()
        {
            char[] charactors = new char[idCharacters.Length];
            charactors = idCharacters.ToCharArray();
            byte[] data = new byte[1];

            KeyGenCore.GetNonZeroBytes(data);
            data = new byte[max_size_request];
            KeyGenCore.GetNonZeroBytes(data);

            StringBuilder ResultKey = new StringBuilder(max_size_request);
            foreach (byte b in data)
            {
                ResultKey.Append(charactors[b % (charactors.Length)]);
            }
            return ResultKey.ToString();
        }
        public static string GenerateQrCode()
        {
            char[] charactors = new char[qrCharacters.Length];
            charactors = qrCharacters.ToCharArray();
            byte[] data = new byte[1];

            KeyGenCore.GetNonZeroBytes(data);
            data = new byte[max_size_qr];
            KeyGenCore.GetNonZeroBytes(data);

            StringBuilder ResultKey = new StringBuilder(max_size_qr);
            foreach (byte b in data)
            {
                ResultKey.Append(charactors[b % (charactors.Length)]);
            }
            return ResultKey.ToString();
        }

    }
}
