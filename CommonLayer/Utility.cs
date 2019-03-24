using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CommonLayer
{
    public class Utility
    {
        public static string MD5Crypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            byte[] textBytes = Encoding.Default.GetBytes(input);
            try
            {
                MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw new Exception("Error.");
            }
        }
    }
}
