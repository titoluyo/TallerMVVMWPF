using System;
using Ocean.Infrastructure;
using Ocean.InputStringFormatting;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents ViewValidationError
    /// </summary>
    public class ViewValidationError {

        #region  Properties

        /// <summary>
        /// Gets or sets the name of the data item.
        /// </summary>
        /// <value>The name of the data item.</value>
        public String DataItemName { get; private set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public String ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public String Key {
            get {
                return string.Format("{0}:{1}", this.DataItemName, this.PropertyName);
            }
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewValidationError"/> class.
        /// </summary>
        /// <param name="dataItemName">Name of the data item.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        public ViewValidationError(String dataItemName, String propertyName, String errorMessage) {
            this.DataItemName = dataItemName;
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Create an error message with properties separated by white space
        /// </summary>
        /// <returns></returns>
        public String ToErrorMessage() {
            return String.Concat(CamelCaseString.GetWords(this.PropertyName), GlobalConstants.STRING_WHITE_SPACE, this.ErrorMessage);
        }

        /// <summary>
        /// Create a friendly error message.
        /// </summary>
        /// <returns></returns>
        public String ToFriendlyErrorMessage() {

            String errorMessage;

            if(this.ErrorMessage.Contains("not recognized as a valid DateTime")) {
                errorMessage = Resources.UIValidationError_ToFriendlyErrorMessage_date_is_not_a_valid_format_;

            } else if(this.ErrorMessage.Contains("not in a correct format.")) {
                errorMessage = Resources.UIValidationError_ToFriendlyErrorMessage_entered_value_is_not_the_correct_data_type_;

                //TODO - developers - add more ElseIf tests here if required to return back the best message possible without using the default

            } else {
                errorMessage = this.ErrorMessage;
            }

            var propertyName = CamelCaseString.GetWords(this.PropertyName.Contains(".") ? this.PropertyName.Substring(this.PropertyName.LastIndexOf(".") + 1) : this.PropertyName);

            return string.Concat(propertyName, GlobalConstants.STRING_WHITE_SPACE, errorMessage);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString() {
            return string.Format(Resources.UIValidationError_ToString_DataItem___0___PropertyName___1___Error___2_FormatString, this.DataItemName, this.PropertyName, this.ErrorMessage);
        }

        #endregion
    }
}
