using System;
using Wpf.Common.Infrastructure;

namespace Wpf.Validation.Infrastructure {

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
        public String ToErrorMessage() {
            return String.Concat(CamelCaseString.GetWords(this.PropertyName), Constants.StringWhiteSpace, this.ErrorMessage);
        }

        /// <summary>
        /// Create a friendly error message.
        /// </summary>
        public String ToFriendlyErrorMessage() {

            String errorMessage;

            if (this.ErrorMessage.Contains("not recognized as a valid DateTime")) {
                errorMessage = "date is not a valid format";

            } else if (this.ErrorMessage.Contains("not in a correct format.")) {
                errorMessage = "entered value is not the correct data type";

                //TODO - developers - add more ElseIf tests here if required to return back the best message possible without using the default

            } else {
                errorMessage = this.ErrorMessage;
            }

            var propertyName = CamelCaseString.GetWords(this.PropertyName.Contains(".") ? this.PropertyName.Substring(this.PropertyName.LastIndexOf(".") + 1) : this.PropertyName);

            return string.Concat(propertyName, Constants.StringWhiteSpace, errorMessage);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString() {
            return string.Format("DataItem {0}, PropertyName {1}, Error {2}", this.DataItemName, this.PropertyName, this.ErrorMessage);
        }

        #endregion
    }
}
