using System;
using System.ComponentModel;
using Ocean.Infrastructure;
using Ocean.Properties;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Provides a container for a single character casing check.
    /// </summary>
    public class CharacterCasingCheck : ObservableObject, IDataErrorInfo, IComparable<CharacterCasingCheck> {

        #region  Declarations

        String _lookFor = String.Empty;
        String _replaceWith = String.Empty;
        const String _LOOKFOR = "LookFor";
        const String _REPLACEWITH = "ReplaceWith";

        #endregion

        #region  Properties

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <value></value>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public String Error {
            get {
                if(_lookFor == null) {
                    _lookFor = String.Empty;
                }

                if(_replaceWith == null) {
                    _replaceWith = String.Empty;
                }

                String error = null;

                if(String.IsNullOrEmpty(_lookFor)) {
                    error = Resources.CharacterCasingCheck_Error_Look_For_is_a_required_field_;
                }

                if(String.IsNullOrEmpty(_replaceWith)) {

                    if(error != null) {
                        error += Environment.NewLine;
                    }

                    error += Resources.CharacterCasingCheck_Error_Replace_With_is_a_required_field_;
                }

                if(_lookFor.Length != _replaceWith.Length) {
                    if(error != null) {
                        error += Environment.NewLine;
                    }

                    error += Resources.CharacterCasingCheck_Error_Look_For_and_Replace_With_must_be_the_same_length_;
                }

                return error ?? String.Empty;
            }
        }

        /// <summary>
        /// Gets the any errors associated with the specified column name.
        /// </summary>
        /// <value></value>
        public String this[String columnName] {
            get {

                if(columnName == _LOOKFOR && String.IsNullOrEmpty(_lookFor)) {
                    return Resources.CharacterCasingCheck_Error_Look_For_is_a_required_field_;
                }


                if(columnName == _REPLACEWITH && String.IsNullOrEmpty(_replaceWith)) {
                    return Resources.CharacterCasingCheck_Error_Replace_With_is_a_required_field_;
                }

                return _lookFor.Length != _replaceWith.Length ? Resources.CharacterCasingCheck_Error_Look_For_and_Replace_With_must_be_the_same_length_ : String.Empty;
            }
        }

        /// <summary>
        /// Gets and sets the String value to look for when the character casing check is being performed.
        /// </summary>
        /// <value>The look for.</value>
        public String LookFor {
            get {
                return _lookFor;
            }
            set {
                _lookFor = value;
                RaisePropertyChanged(_LOOKFOR);
                RaisePropertyChanged(_REPLACEWITH);
            }
        }

        /// <summary>
        /// Gets and sets the String value that will replace the LookFor value when the character casing check is being performed.
        /// </summary>
        /// <value>The replace with.</value>
        public String ReplaceWith {
            get {
                return _replaceWith;
            }
            set {
                _replaceWith = value;
                RaisePropertyChanged(_REPLACEWITH);
                RaisePropertyChanged(_LOOKFOR);
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingCheck"/> class.
        /// </summary>
        public CharacterCasingCheck() { }

        /// <summary>
        /// Provides a container for a custom character casing correction.  The LookFor and ReplaceWith strings must be the same length.
        /// </summary>
        /// <param name="lookFor">String value to replace.</param>
        /// <param name="replaceWith">String value that will replace the LookFor value.</param>
        public CharacterCasingCheck(String lookFor, String replaceWith) {

            if(lookFor.Length != replaceWith.Length) {
                throw new ArgumentException(Resources.CharacterCasingCheck_CharacterCasingCheck_The_LookFor_and_ReplaceWith_strings_must_be_the_same_length_);
            }

            _lookFor = lookFor;
            _replaceWith = replaceWith;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Int32 CompareTo(CharacterCasingCheck other) {
            return _lookFor.CompareTo(other.LookFor);
        }

        #endregion

    }
}