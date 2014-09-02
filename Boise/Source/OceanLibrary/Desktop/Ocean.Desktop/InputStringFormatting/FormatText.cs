using System;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents FormatText, provides text formatting 
    /// </summary>
    public static class FormatText {

        #region  Constructors

        /// <summary>
        /// Initializes the <see cref="FormatText"/> class.
        /// </summary>
        static FormatText() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Corrects the text character casing and optionally format phone fields simliar to Microsoft Outlook.
        /// </summary>
        /// <param name="strIn">String to be case corrected and optionally formatted.</param>
        /// <param name="characterCase">Character case and format.</param>
        /// <returns>String case corrected and optionally formatted.</returns>
        public static String ApplyCharacterCasing(String strIn, CharacterCasing characterCase) {
            strIn = strIn.Trim();

            if (strIn.Length == 0) {
                return strIn;
            }

            Int32 intX;

            switch (characterCase) {

                case CharacterCasing.None:
                    return strIn;

                case CharacterCasing.LowerCase:
                    return strIn.ToLower();

                case CharacterCasing.UpperCase:
                    return strIn.ToUpper();

                case CharacterCasing.OutlookPhoneNoProperName:
                    return FormatOutLookPhone(strIn);

                case CharacterCasing.OutlookPhoneUpper:
                    return FormatOutLookPhone(strIn).ToUpper();
            }

            strIn = strIn.ToLower();

            String strPrevious = " ";
            String strPreviousTwo = "  ";
            String strPreviousThree = "   ";
            String strChar;

            for (intX = 0; intX < strIn.Length; intX++) {
                strChar = strIn.Substring(intX, 1);

                if (char.IsLetter(Convert.ToChar(strChar)) && strChar != strChar.ToUpper()) {

                    if (strPrevious == " " || strPrevious == "." || strPrevious == "-" || strPrevious == "/" || strPreviousThree == " O'" || strPreviousTwo == "Mc") {
                        strIn = strIn.Remove(intX, 1);
                        strIn = strIn.Insert(intX, strChar.ToUpper());
                        strPrevious = strChar.ToUpper();

                    } else {
                        strPrevious = strChar;
                    }

                } else {
                    strPrevious = strChar;
                }

                strPreviousTwo = strPreviousTwo.Substring(1, 1) + strPrevious;
                strPreviousThree = strPreviousThree.Substring(1, 1) + strPreviousThree.Substring(2, 1) + strPrevious;
            }

            intX = strIn.IndexOf("'");

            if (intX == 1) {

                String strInsert = strIn.Substring(2, 1).ToUpper();
                strIn = strIn.Remove(2, 1);
                strIn = strIn.Insert(2, strInsert);
            }

            try {
                intX = strIn.IndexOf("'", 3);

                if (intX > 3 && strIn.Substring(intX - 2, 1) == " ") {

                    String strInsert = strIn.Substring(intX + 1, 1).ToUpper();
                    strIn = strIn.Remove(intX + 1, 1);
                    strIn = strIn.Insert(intX + 1, strInsert);
                }

// ReSharper disable EmptyGeneralCatchClause
            } catch {
// ReSharper restore EmptyGeneralCatchClause
            }

            //never remove this code
            strIn += " ";

            foreach (CharacterCasingCheck check in CharacterCasingChecks.GetChecks()) {

                if (strIn.Contains(check.LookFor)) {

                    Int32 intPosition = strIn.IndexOf(check.LookFor);

                    if (intPosition > -1) {
                        strIn = strIn.Remove(intPosition, check.LookFor.Length);
                        strIn = strIn.Insert(intPosition, check.ReplaceWith);
                    }

                }

            }

            if (characterCase == CharacterCasing.OutlookPhoneProperName) {
                strIn = FormatOutLookPhone(strIn);
            }

            return strIn.Trim();
        }

        static String FormatOutLookPhone(String strIn) {

            if (strIn.Trim().Length == 0) {
                return strIn;
            }

            String tempCasted = strIn + " ";

            try {

                String temp = tempCasted;
                Int32 intX = temp.IndexOf(" ", 8);

                if (intX > 0) {
                    temp = strIn.Substring(0, intX);
                    temp = temp.Replace("(", "");
                    temp = temp.Replace(")", "");
                    temp = temp.Replace(" ", "");
                    temp = temp.Replace("-", "");

                    Int64 lngTemp;

                    if (Int64.TryParse(temp, out lngTemp) && temp.Length == 10) {
                        tempCasted = lngTemp.ToString("(###) ###-####") + "  " + tempCasted.Substring(intX).Trim();
                    }

                }
// ReSharper disable EmptyGeneralCatchClause
            } catch {
// ReSharper restore EmptyGeneralCatchClause
            }

            return tempCasted;
        }

        #endregion
    }
}