using System;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents the CharacterCasingChecks, provides container for all application character casing correction rules.  This class is consumed by the BusinessEntityBase class when it applies CharacterCasingFormatting rules to a property when it's changed.
    /// </summary>
    public class CharacterCasingChecks : System.Collections.Generic.List<CharacterCasingCheck> {

        #region  Declarations

        static CharacterCasingChecks _instance;
        static Func<CharacterCasingChecks> _getChecksSource;

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingChecks"/> class.
        /// </summary>
        CharacterCasingChecks() { }

        #endregion

        #region  Methods

        /// <summary>
        ///  Returns a list of CharacterCasingCheck objects that are utilized when a CharacterCasingFormatting rule is applied to a property.
        /// </summary>
        public static CharacterCasingChecks GetChecks() {

            if (_getChecksSource != null) {
                return _getChecksSource.Invoke();
            }

            if (_instance == null) {
// ReSharper disable UseObjectOrCollectionInitializer
                _instance = new CharacterCasingChecks();
// ReSharper restore UseObjectOrCollectionInitializer
                //TODO - developers - you can load this from a data base, config file, web service, etc.
                //  See SetGetChecksSource below.  BBQ Shack uses SetGetChecksSource.
                //
                //You can also add, remove or edit these by modifying the CharacterCasingChecks collection from your application.
                //
                //These are values that are specific to your company or line of business
                //remove the ones that don't apply and add your own.
                //ensure that the lengths of the LookFor and the ReplaceWith strings are the same
                _instance.Add(new CharacterCasingCheck("Po Box", "PO Box"));
                _instance.Add(new CharacterCasingCheck("C/o ", "c/o "));
                _instance.Add(new CharacterCasingCheck("C/O ", "c/o "));
                _instance.Add(new CharacterCasingCheck("Vpn ", "VPN "));
                _instance.Add(new CharacterCasingCheck("Xp ", "XP "));
                _instance.Add(new CharacterCasingCheck(" Or ", " or "));
                _instance.Add(new CharacterCasingCheck(" And ", " and "));
                _instance.Add(new CharacterCasingCheck(" Nw ", " NW "));
                _instance.Add(new CharacterCasingCheck(" Ne ", " NE "));
                _instance.Add(new CharacterCasingCheck(" Sw ", " SW "));
                _instance.Add(new CharacterCasingCheck(" Se ", " SE "));
                _instance.Add(new CharacterCasingCheck(" Llc. ", " LLC. "));
                _instance.Add(new CharacterCasingCheck(" Llc ", " LLC "));
                _instance.Add(new CharacterCasingCheck(" Lc ", " LC "));
                _instance.Add(new CharacterCasingCheck(" Lc. ", " LC. "));
                _instance.Add(new CharacterCasingCheck("Wpf", "WPF"));
            }

            return _instance;
        }

        /// <summary>
        /// Provides an injection point for an alternate source of CharacterCasingChecks at runtime
        /// </summary>
        /// <param name="source">Func(Of CharacterCasingChecks)</param>
        public static CharacterCasingChecks SetGetChecksSource(Func<CharacterCasingChecks> source) {
            _getChecksSource = source;
            return new CharacterCasingChecks();
        }

        #endregion
    }
}

