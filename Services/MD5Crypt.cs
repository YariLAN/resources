using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class MD5Crypt
    {
        private readonly MD5 mD5;
        public MD5Crypt() 
        {
            mD5 = MD5.Create();
        }
        public string GetHashToPassword(string password)
        {
            byte[] b = Encoding.UTF8.GetBytes(password);

            var hash = mD5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();

            foreach (var symbols in hash)
                sb.Append(symbols.ToString("X2"));

            return sb.ToString();
        }
    }
}
