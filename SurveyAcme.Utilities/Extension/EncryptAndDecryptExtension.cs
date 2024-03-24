using System.Security.Cryptography;
using System.Text;

namespace SurveyAcme.Utilities.Extension
{
    public static class EncryptAndDecryptExtension
    {
        public static string key = "SurveyAcme2024$$";
        private const int ks = 128;
        private const int di = 1000;
        private const int bufferSize = 4096;

        public static string EncryptByAES(this string x)
        {
            try
            {
                var a = Generate128BitsOfRandomEntropy();
                var b = Generate128BitsOfRandomEntropy();
                var c = Encoding.UTF8.GetBytes(x);
                var e = GenerateE(a);
                return Convert.ToBase64String(GenerateMemoryStreamEncrypt(a, e, b, c));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public static string DecryptByAES(this string x)
        {
            try
            {
                var a = Convert.FromBase64String(x);
                var b = a.Take(ks / 8).ToArray();
                var c = a.Skip(ks / 8).Take(ks / 8).ToArray();
                var d = a.Skip((ks / 8) * 2).Take(a.Length - ((ks / 8) * 2)).ToArray();

                return Encoding.UTF8.GetString(GenerateMemoryStreamDecrypt(b, c, d));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        static byte[] Generate128BitsOfRandomEntropy()
        {
            var x = new byte[16];
            using (var y = new RNGCryptoServiceProvider())
            {
                y.GetBytes(x);
            }
            return x;
        }

        private static byte[] GenerateE(byte[] a)
        {
            using var d = new Rfc2898DeriveBytes(key, a, di);
            var e = d.GetBytes(ks / 8);
            return e;
        }

        private static byte[] GenerateMemoryStreamEncrypt(byte[] a, byte[] e, byte[] b, byte[] c)
        {
            var f = GenerateAes();
            using var g = f.CreateEncryptor(e, b);
            using var h = new MemoryStream();
            using var i = new CryptoStream(h, g, CryptoStreamMode.Write);
            i.Write(c, 0, c.Length);
            i.FlushFinalBlock();
            var j = a;
            j = j.Concat(b).ToArray();
            j = j.Concat(h.ToArray()).ToArray();
            h.Close();
            i.Close();
            return j;
        }

        private static byte[] GenerateMemoryStreamDecrypt(byte[] b, byte[] c, byte[] d)
        {
            var g = GenerateAes();
            var f = GenerateE(b);
            using var h = g.CreateDecryptor(f, c);
            using var i = new MemoryStream(d);
            var buffer = new byte[bufferSize];
            var bytesRead = 0;
            var resultBytes = new List<byte>();
            using (var j = new CryptoStream(i, h, CryptoStreamMode.Read))
            {
                while ((bytesRead = j.Read(buffer, 0, bufferSize)) > 0)
                {
                    resultBytes.AddRange(buffer.Take(bytesRead));
                }
            }
            return resultBytes.ToArray();
        }

        private static Aes GenerateAes()
        {
            var f = Aes.Create();
            f.BlockSize = 128;
            f.Mode = CipherMode.CBC;
            f.Padding = PaddingMode.PKCS7;
            return f;
        }


    }
}
