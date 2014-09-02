using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents RegularExpressionValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RegularExpressionValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets the custom regular expression pattern.
        /// </summary>
        /// <value>The custom regular expression pattern.</value>
        public String CustomRegularExpressionPattern { get; private set; }

        /// <summary>
        /// Gets the type of the regular expression pattern.
        /// </summary>
        /// <value>The type of the regular expression pattern.</value>
        public RegularExpressionPatternType RegularExpressionPatternType { get; private set; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute"/> class.
        /// </summary>
        /// <param name="regularExpressionPatternType">Type of the regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RegularExpressionValidatorAttribute(RegularExpressionPatternType regularExpressionPatternType, RequiredEntry requiredEntry) {
            this.RegularExpressionPatternType = regularExpressionPatternType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute"/> class.
        /// </summary>
        /// <param name="customRegularExpressionPattern">The custom regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RegularExpressionValidatorAttribute(String customRegularExpressionPattern, RequiredEntry requiredEntry) {
            this.RegularExpressionPatternType = RegularExpressionPatternType.Custom;
            this.RequiredEntry = requiredEntry;

            if (!(StringValidationRules.IsRegularExpressionPatternValid(customRegularExpressionPattern))) {
                throw new ArgumentOutOfRangeException("customRegularExpressionPattern", Resources.RegularExpressionValidatorAttribute_RegularExpressionValidatorAttribute_Programmer_did_not_supply_a_valid_CustomRegularExpressionPattern);
            }

            this.CustomRegularExpressionPattern = customRegularExpressionPattern;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(StringValidationRules.RegularExpressionRule, new RegularExpressionRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}