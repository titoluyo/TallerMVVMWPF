
using System;
using System.Text;

namespace Simple.Core.Infrastructure {

    public enum WordSpacerOption { None, StripeLeadingNonUpper }

    /// <summary>
    /// Represents extensions to the String Class
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Inserts a space before each upper case character except the first instance of an upper case character
        /// </summary>
        /// <param name="stringIn">The <see cref="System.String"/> that initiated the call to this method and that will be formatted.</param>
        /// <param name="option">None - all characters will be spaced and returned.  StripeLeadingNonUpper - will remove all leading characters until an upper case character is found, then return the remaining characters spaced.</param>
        /// <returns><see cref="System.String"/> formatted in accordance with the <paramref name="option"/> rules.</returns>
        public static String WordSpacer(this String stringIn, WordSpacerOption option = WordSpacerOption.None) {
            StringBuilder sb = new StringBuilder();
            Boolean foundUpper;

            if (option == WordSpacerOption.None)
                foundUpper = true;

            else
                foundUpper = false;

            foreach (Char c in stringIn) {

                if (foundUpper) {

                    if (Char.IsUpper(c)) {
                        sb.Append(" ");
                        sb.Append(c);
                    } else if (char.IsLetterOrDigit(c)) {
                        sb.Append(c);
                    }
                } else if (Char.IsUpper(c)) {
                    foundUpper = true;
                    sb.Append(c);
                }
            }

            if (!foundUpper)
                return stringIn;

            else
                return sb.ToString();
        }
    }
}
