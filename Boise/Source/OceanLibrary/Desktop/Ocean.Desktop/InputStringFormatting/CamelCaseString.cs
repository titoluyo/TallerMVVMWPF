using System;
using Ocean.Infrastructure;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents CamelCaseString
    /// </summary>
    public static class CamelCaseString {

        #region  Constructors

        /// <summary>
        /// Initializes the <see cref="CamelCaseString"/> class.
        /// </summary>
        static CamelCaseString() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Designed to parse property or database column names and return a friendly name without punctuation characters.  Example:  "ap_c_FirstName" will result in "First Name"
        /// </summary>
        /// <returns>String with words parsed from camel case String and space added between words.</returns>
        public static String GetWords(String camel) {

            var sb = new System.Text.StringBuilder(256);
            Boolean foundUpper = false;

            foreach (char c in camel) {

                if (foundUpper) {

                    if (char.IsUpper(c)) {
                        sb.Append(GlobalConstants.STRING_WHITE_SPACE);
                        sb.Append(c);

                    } else if (char.IsLetterOrDigit(c)) {
                        sb.Append(c);
                    }

                } else if (char.IsUpper(c)) {
                    foundUpper = true;
                    sb.Append(c);
                }

            }

            return sb.ToString();
        }

        #endregion
    }
}

