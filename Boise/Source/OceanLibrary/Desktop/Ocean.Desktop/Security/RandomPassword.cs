
using System;
using System.Security.Cryptography;
using Ocean.Properties;


namespace Ocean.Security {

    ///<summary>
    ///This class can generate random passwords, which do not include ambiguous 
    ///characters, such as I, l, and 1. The generated password will be made of
    ///7-bit ASCII symbols. Every four characters will include one lower case
    ///character, one upper case character, one number, and one special symbol
    ///(such as '%') in a random order. The password will always start with an
    ///alpha-numeric character; it will not start with a special symbol (we do
    ///this because some back-end systems do not like certain special
    ///characters in the first position).
    ///</summary>
    public class RandomPassword {

        #region  Declarations

        const Int32 _DEFAULT_MAX_PASSWORD_LENGTH = 10;

        ///Define default min and max password lengths.
        const Int32 _DEFAULT_MIN_PASSWORD_LENGTH = 8;

        ///Define supported password characters divided into groups.
        ///You can add (or remove) characters to (from) these groups.
        static String _passwordCharsLcase = "abcdefgijkmnopqrstwxyz";
        static String _passwordCharsNumeric = "23456789";
        static String _passwordCharsSpecial = "!@#$";
        static String _passwordCharsUcase = "ABCDEFGHJKLMNPQRSTWXYZ";

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPassword"/> class.
        /// </summary>
        RandomPassword() { }

        #endregion

        #region  Methods

        ///<summary>
        ///Generates a random password.
        ///</summary>
        ///<returns>
        ///Randomly generated password.
        ///</returns>
        ///<remarks>
        ///The length of the generated password will be determined at
        ///random. It will be no shorter than the minimum default and
        ///no longer than maximum default.
        ///</remarks>
        public static String Generate() {
            return Generate(_DEFAULT_MIN_PASSWORD_LENGTH, _DEFAULT_MAX_PASSWORD_LENGTH);
        }

        ///<summary>
        ///Generates a random password.
        ///</summary>
        ///<returns>
        ///Randomly generated password.
        ///</returns>
        ///<remarks>
        ///The length of the generated password will be determined at
        ///random. It will be no shorter than the minimum default and
        ///no longer than maximum default.
        ///</remarks>
        public static String Generate(Int32 length, Boolean includeSpecialCharacters, Boolean includeNumbers, Boolean includeUpperCase, Boolean includeLowerCase) {

            if(includeLowerCase == false && includeNumbers == false && includeSpecialCharacters == false && includeUpperCase == false) {
                return Resources.RandomPassword_Generate_tryagain;
            }

            if(!includeSpecialCharacters) {
                _passwordCharsSpecial = String.Empty;
            }

            if(!includeNumbers) {
                _passwordCharsNumeric = String.Empty;
            }

            if(!includeUpperCase) {
                _passwordCharsUcase = String.Empty;
            }

            if(!includeLowerCase) {
                _passwordCharsLcase = String.Empty;
            }

            return Generate(length, length);
        }

        ///<summary>
        ///Generates a random password.
        ///</summary>
        ///<param name="minLength">
        ///Minimum password length.
        ///</param>
        ///<param name="maxLength">
        ///Maximum password length.
        ///</param>
        ///<returns>
        ///Randomly generated password.
        ///</returns>
        ///<remarks>
        ///The length of the generated password will be determined at
        ///random and it will fall with the range determined by the
        ///function parameters.
        ///</remarks>
        public static String Generate(int minLength, int maxLength) {

            //Create a local array containing supported password characters
            //grouped by types. You can remove character groups from this
            //array, but doing so will weaken the password strength.
            var charGroups = new[] { _passwordCharsLcase.ToCharArray(), _passwordCharsUcase.ToCharArray(), _passwordCharsNumeric.ToCharArray(), _passwordCharsSpecial.ToCharArray() };

            //Use this array to track the number of unused characters in each
            //character group.
            var charsLeftInGroup = new int[charGroups.Length];

            //Initially, all characters in each group are not used.
            Int32 i;

            for(i = 0; i < charsLeftInGroup.Length; i++) {
                charsLeftInGroup[i] = charGroups[i].Length;
            }

            //Use this array to track (iterate through) unused character groups.
            var leftGroupsOrder = new int[charGroups.Length];

            //Initially, all character groups are not used.
            for(i = 0; i < leftGroupsOrder.Length; i++) {
                leftGroupsOrder[i] = i;
            }

            //Because we cannot use the default randomizer, which is based on the
            //current time (it will produce the same "random" number within a
            //second), we will use a random number generator to seed the
            //randomizer.
            //Use a 4-byte array to fill it with random bytes and convert it then
            //to an integer value.
            var randomBytes = new byte[4];

            //Generate 4 random bytes.
            using(var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(randomBytes);
            }

            //Convert 4 bytes into a 32-bit integer value.
            Int32 seed = ((randomBytes[0] & 0X7F) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3]);

            //Now, this is real randomization.
            var random = new Random(seed);

            //Allocate appropriate memory for the password.
            char[] password = minLength < maxLength ? new Char[random.Next(minLength - 1, maxLength) + 1] : new Char[minLength];

            //Index of the next character to be added to password.
            Int32 nextCharIdx;

            //Index of the next character group to be processed.
            Int32 nextGroupIdx;

            //Index which will be used to track not processed character groups.
            Int32 nextLeftGroupsOrderIdx;

            //Index of the last non-processed character in a group.
            Int32 lastCharIdx;

            //Index of the last non-processed group.
            Int32 lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            //Generate password characters one at a time.
            for(i = 0; i < password.Length; i++) {

                //If only one character group remained unprocessed, process it;
                //otherwise, pick a random character group from the unprocessed
                //group list. To allow a special character to appear in the
                //first position, increment the second parameter of the Next
                //function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);

                //Get the actual index of the character group, from which we will
                //pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                //Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                //If only one unprocessed character is left, pick it; otherwise,
                //get a random character from the unused character list.
                nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);

                //Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                //If we processed the last character in this group, start over.
                if(lastCharIdx == 0) {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;

                    //There are more unprocessed characters left.
                } else {

                    //Swap processed character with the last unprocessed character
                    //so that we don't pick it until we process all characters in
                    //this group.
                    if(lastCharIdx != nextCharIdx) {

                        Char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }

                    //Decrement the number of unprocessed characters in
                    //this group.
                    charsLeftInGroup[nextGroupIdx] = charsLeftInGroup[nextGroupIdx] - 1;
                }

                //If we processed the last group, start all over.
                if(lastLeftGroupsOrderIdx == 0) {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

                    //There are more unprocessed groups left.
                } else {

                    //Swap processed group with the last unprocessed group
                    //so that we don't pick it until we process all groups.
                    if(lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx) {

                        Int32 temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }

                    //Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx = lastLeftGroupsOrderIdx - 1;
                }
            }
            //Convert password characters into a String and return the result.
            return new String(password);
        }

        ///<summary>
        ///Generates a random password of the exact length.
        ///</summary>
        ///<param name="length">
        ///Exact password length.
        ///</param>
        ///<returns>
        ///Randomly generated password.
        ///</returns>
        public static String Generate(Int32 length) {
            return Generate(length, length);
        }

        #endregion
    }
}

