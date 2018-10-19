using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Soteria.DataComponents.Infrastructure.Common
{
    public class Utility
    {
        public static string NullToString(object objInput)
        {
            if (objInput == null || objInput == DBNull.Value)
                return (string.Empty);
            else
                return (objInput.ToString().Trim());
        }

        public static int NullToInt(object objInput)
        {
            int intAttemptedInteger = Convert.ToInt32(Decimal.Zero);
            if (!(objInput == null || objInput == DBNull.Value))
            {
                Int32.TryParse(objInput.ToString(), out intAttemptedInteger);
            }

            return (intAttemptedInteger);
        }

        public static bool NullToBool(object objInput)
        {
            if (objInput is DBNull || objInput == null)
                return false;
            else
                return Convert.ToBoolean(objInput);
        }

        public static decimal NullToDecimal(object objInput)
        {
            if (objInput is DBNull || objInput == null)
            {
                return 0;
            }
            if (objInput is string && ((string)objInput).Length == 0)
            {
                return 0;
            }
            return Convert.ToDecimal(objInput);
        }
        public static DateTime NullToDefaultDateTime(object objInput)
        {
            if (objInput is DBNull || objInput == null)
            {
                return DateTime.Now;
            }
            if (objInput is string && ((string)objInput).Length == 0)
            {
                return DateTime.Now;
            }
            return Convert.ToDateTime(objInput);
        }
        public static DateTime? NullToDateTime(object objInput)
        {
            if (objInput is DBNull || objInput == null)
            {
                return null;
            }
            if (objInput is string && ((string)objInput).Length == 0)
            {
                return null;
            }
            return Convert.ToDateTime(objInput);
        }


        public static string GetEntityXml(Object parameter)
        {
            XmlSerializer xs = new XmlSerializer(parameter.GetType());
            using (StringWriter stream = new StringWriter())
            {
                xs.Serialize(stream, parameter);
                stream.Flush();
                return stream.ToString();
            }

        }

        public static string GetIPAddress()
        {
            return GetIPAddress(new HttpRequestWrapper(HttpContext.Current.Request));
        }

        internal static string GetIPAddress(HttpRequestBase request)
        {
            // handle standardized 'Forwarded' header
            string forwarded = request.Headers["Forwarded"];
            if (!String.IsNullOrEmpty(forwarded))
            {
                foreach (string segment in forwarded.Split(',')[0].Split(';'))
                {
                    string[] pair = segment.Trim().Split('=');
                    if (pair.Length == 2 && pair[0].Equals("for", StringComparison.OrdinalIgnoreCase))
                    {
                        string ip = pair[1].Trim('"');

                        // IPv6 addresses are always enclosed in square brackets
                        int left = ip.IndexOf('['), right = ip.IndexOf(']');
                        if (left == 0 && right > 0)
                        {
                            return ip.Substring(1, right - 1);
                        }

                        // strip port of IPv4 addresses
                        int colon = ip.IndexOf(':');
                        if (colon != -1)
                        {
                            return ip.Substring(0, colon);
                        }

                        // this will return IPv4, "unknown", and obfuscated addresses
                        return ip;
                    }
                }
            }

            // handle non-standardized 'X-Forwarded-For' header
            string xForwardedFor = request.Headers["X-Forwarded-For"];
            if (!String.IsNullOrEmpty(xForwardedFor))
            {
                return xForwardedFor.Split(',')[0];
            }

            return request.UserHostAddress;
        }
    }
}
