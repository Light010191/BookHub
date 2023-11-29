using System.Security.Cryptography;
using System.Text;

namespace BooksHub.Services
{
    public static class SHA256Encripter
    {

        public static string Encript(string s)
        {
            using (SHA256 sHA = SHA256.Create())
            {
                byte[] bytes = sHA.ComputeHash(Encoding.UTF8.GetBytes(s));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
