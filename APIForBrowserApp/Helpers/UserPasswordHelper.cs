using System.Security.Cryptography;
using System.Text;

namespace APIForBrowserApp.Helpers
{
    public static class UserPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
        }
    }
}
