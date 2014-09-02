using System;
using System.Text;

namespace Wpf.Common.Infrastructure {
    /// <summary>
    /// Helper class to easily build Uri strings used in Prism navigation.
    /// The last parameter takes an array of key, value pairs.
    /// </summary>
    public class QueryStringBuilder {

        private QueryStringBuilder() {
        }

        /// <summary>
        /// Constructs the specified Uri.
        /// </summary>
        /// <param name="protocol">The protocol.  Example:  http://</param>
        /// <param name="address">The address. Example www.bing.com or ApplicationMenuView</param>
        /// <param name="parms">The parms.
        /// <code>string[,] parms = { {"itemId", "1255"}, {"action", "edit"} }</code>
        /// </param>
        /// <returns></returns>
        public static String Construct(String protocol, String address, String[,] parms) {
            return String.Concat(protocol, address, ParseParameters(parms));
        }

        /// <summary>
        /// Constructs the specified Uri.
        /// </summary>
        /// <param name="address">The address. Example www.bing.com or ApplicationMenuView</param>
        /// <param name="parms">The parms.
        /// <code>string[,] parms = { {"itemId", "1255"}, {"action", "edit"} }</code>        
        /// </param>
        /// <returns></returns>
        public static String Construct(String address, String[,] parms) {
            return String.Concat(address, ParseParameters(parms));
        }

        static String ParseParameters(String[,] parms) {
            if(parms == null || parms.Length == 0) {
                return String.Empty;
            }

            var sb = new StringBuilder();
            String token = "?";

            for(Int32 i = 0; i < parms.Length - 1; i++) {
                sb.AppendFormat("{0}{1}={2}", token, parms[i, 0], parms[i, 1]);
                token = "&";
            }
            return sb.ToString();
        }
    }
}
