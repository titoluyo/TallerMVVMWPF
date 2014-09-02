using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents RegularExpressionRuleDescriptor
    /// </summary>
    public class RegularExpressionRuleDescriptor : RuleDescriptorBase {

        #region  Declarations

        String _customRegularExpressionPattern = String.Empty;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the custom regular expression pattern.
        /// </summary>
        /// <value>The custom regular expression pattern.</value>
        public String CustomRegularExpressionPattern {
            get {
                return _customRegularExpressionPattern;
            }
            set {

                if (String.IsNullOrEmpty(value)) {
                    _customRegularExpressionPattern = String.Empty;
                    return;
                }

                if (this.RegularExpressionPatternType != RegularExpressionPatternType.Custom) {
                    throw new InvalidOperationException(Resources.RegularExpressionRuleDescriptor_CustomRegularExpressionPattern_Before_setting_a_custom_pattern__the_Pattern_Type_must_be_custom_);
                }

                if (!(StringValidationRules.IsRegularExpressionPatternValid(value))) {
                    throw new InvalidOperationException(Resources.RegularExpressionRuleDescriptor_CustomRegularExpressionPattern_value_is_not_a_valid_regular_expression_);
                }

                _customRegularExpressionPattern = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the regular expression pattern.
        /// </summary>
        /// <value>The type of the regular expression pattern.</value>
        public RegularExpressionPatternType RegularExpressionPatternType { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor"/> class.
        /// </summary>
        /// <param name="regularExpressionPatternType">Type of the regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public RegularExpressionRuleDescriptor(RegularExpressionPatternType regularExpressionPatternType, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(customMessage, propertyFriendlyName, propertyName, ruleSet, overrideMessage) {

            if (regularExpressionPatternType == RegularExpressionPatternType.Custom) {
                throw new InvalidOperationException(Resources.RegularExpressionRuleDescriptor_RegularExpressionRuleDescriptor_This_constructor_does_not_support_assigning_the_RegularExpressionPatternType_to_Custom___Use_other_constructor_);
            }

            this.RegularExpressionPatternType = regularExpressionPatternType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public RegularExpressionRuleDescriptor(RegularExpressionValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.RegularExpressionPatternType = e.RegularExpressionPatternType;

            if (e.RegularExpressionPatternType == RegularExpressionPatternType.Custom) {

                if (!(StringValidationRules.IsRegularExpressionPatternValid(e.CustomRegularExpressionPattern))) {
                    throw new InvalidOperationException(Resources.RegularExpressionRuleDescriptor_CustomRegularExpressionPattern_value_is_not_a_valid_regular_expression_);
                }

                _customRegularExpressionPattern = e.CustomRegularExpressionPattern;
            }

            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionRuleDescriptor"/> class.
        /// </summary>
        /// <param name="customRegularExpressionPattern">The custom regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public RegularExpressionRuleDescriptor(String customRegularExpressionPattern, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(customMessage, propertyFriendlyName, propertyName, ruleSet, overrideMessage) {
            this.RegularExpressionPatternType = RegularExpressionPatternType.Custom;
            this.RequiredEntry = requiredEntry;

            if (!(StringValidationRules.IsRegularExpressionPatternValid(customRegularExpressionPattern))) {
                throw new InvalidOperationException(Resources.RegularExpressionRuleDescriptor_CustomRegularExpressionPattern_value_is_not_a_valid_regular_expression_);
            }

            _customRegularExpressionPattern = customRegularExpressionPattern;
        }

        #endregion
    }
}