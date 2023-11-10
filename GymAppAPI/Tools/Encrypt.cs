using System.Text;
using System.Security.Cryptography;

namespace GymAppAPI.Tools
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sh256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder stringBuilder = new StringBuilder();
            stream = sh256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; ++i) stringBuilder.AppendFormat("{0:x2}", stream[i]);

            return stringBuilder.ToString();
        }


        public static string CalculateSHA256(string str)
        {
            using (SHA256 sha256 = SHA256Managed.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
