using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Soteria.DataComponents.Infrastructure;

namespace Soteria.TVApp.Helper
{
    public class BaseControllerHelper
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char item in str)
            {
                if ((item >= '0' && item <= '9') || (item >= 'A' && item <= 'Z') || (item >= 'a' && item <= 'z') || item == '.' || item == '_')
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }
        internal static string EncryptValue(string value)
        {
            try
            {
                string encryptedValue = Cryptography.Encrypt(value);
                return encryptedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal static string DecryptValue(string value)
        {
            try
            {
                string dencryptedValue = Cryptography.Decrypt(value);
                return dencryptedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string EncodeInput(string inputValue)
        {
            string encodedinputValue = HttpUtility.HtmlEncode(inputValue);
            return encodedinputValue;
        }
        public static string DecodeOutput(string outputValue)
        {
            string decodedoutputValue = HttpUtility.HtmlDecode(outputValue);
            return decodedoutputValue;
        }
    }
}
