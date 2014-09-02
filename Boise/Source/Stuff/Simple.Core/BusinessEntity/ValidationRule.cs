
using System;
using System.ComponentModel.DataAnnotations;
using Simple.Core.Infrastructure;

namespace Simple.Core.BusinessEntity {

    /// <summary>
    /// Represents a validation rule applied to a property
    /// </summary>
    public class ValidationRule {

        #region Declarations

        ValidationAttribute _validationAttribute;

        #endregion

        #region Properties

        /// <summary>
        /// Gets either the error message set in the <see cref="ValidationAttribute"/> or an auto-generated error message with the property name 
        /// formatted to return a user friendly property name.
        /// </summary>
        /// <value>The error message.</value>
        /// <remarks>
        /// When formatting the property name to a friendly name, the <see cref="WordSpacer"/> <see cref="System.String"/> extension will stripe out 
        /// all leading characters until an upper case character is found.  It will then return the remaining characters with a space inserted before 
        /// each upper case character.
        /// </remarks>
        public String ErrorMessage {
            get {

                if (!String.IsNullOrWhiteSpace(_validationAttribute.ErrorMessage))
                    return _validationAttribute.ErrorMessage;

                else
                    return _validationAttribute.FormatErrorMessage(PropertyName.WordSpacer(WordSpacerOption.StripeLeadingNonUpper));
            }
        }

        /// <summary>
        /// Gets the name of the property associated with this <see cref="ValidationRule"/>
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="validationAttribute"><see cref="ValidationAttribute"/></param>
        public ValidationRule(String propertyName, ValidationAttribute validationAttribute) {
            PropertyName = propertyName;
            _validationAttribute = validationAttribute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the passed in value using the <see cref="ValidationRule" /> IsValid method.
        /// </summary>
        /// <param name="value">Value to run rule against</param>
        /// <returns><see langword="true" /> if the value conforms to the rule, otherwise <see langword="false" />.</returns>
        public Boolean IsValid(Object value) {
            return _validationAttribute.IsValid(value);
        }

        #endregion
    }
}
