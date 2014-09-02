
using System;
using System.Security.Cryptography;
using System.Text;

namespace Ocean.Security {

    /// <summary>
    /// Represents Password, set of password related methods
    /// </summary>
    public class Password {

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        Password() {
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the password hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Computed hash for password in byte array</returns>
        public static Byte[] GetPasswordHash(String password) {
            using(var sha256 = SHA256.Create()) {
                return sha256.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
        }

        /// <summary>
        /// Hashes to hex.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>Hex String of the byte array hash</returns>
        public static String HashToHex(Byte[] hash) {

            var sb = new StringBuilder(hash.Length);

            for(int intX = 0; intX < hash.Length; intX++) {
                sb.Append(hash[intX].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Verifies if the password matches the hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hash">The hash.</param>
        public static Boolean Match(String password, Byte[] hash) {

            Byte[] loginHash = GetPasswordHash(password);

            if(loginHash.Length != hash.Length) {
                return false;
            }

            Int32 intX = 0;

            while(intX < loginHash.Length && loginHash[intX] == hash[intX]) {
                intX += 1;

                if(intX == loginHash.Length) {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}