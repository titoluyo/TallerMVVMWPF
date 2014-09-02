using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents StringLengthRuleDescriptor
    /// </summary>
    public class StringLengthRuleDescriptor : RuleDescriptorBase {

        #region Declarations

        const Int32 _MINUS_ONE = -1;
        const Int32 _ONE = 1;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the allow null String.
        /// </summary>
        /// <value>The allow null String.</value>
        public AllowNullString AllowNullString { get; set; }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor"/> class.
        /// </summary>
        /// <param name="allowNullString">The allow null String.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public StringLengthRuleDescriptor(AllowNullString allowNullString, Int32 minimumLength, Int32 maximumLength, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {

            if (maximumLength < _ONE) {
                throw new ArgumentOutOfRangeException("maximumLength", Resources.StringLengthRuleDescriptor_StringLengthRuleDescriptor_must_be_greater_than_0_);
            }

            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException("maximumLength", Resources.StringLengthRuleDescriptor_StringLengthRuleDescriptor_must_be_greater_than_or_equal_to_the_MinimumLength_);
            }

            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor"/> class.
        /// </summary>
        /// <param name="allowNullString">The allow null String.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public StringLengthRuleDescriptor(AllowNullString allowNullString, Int32 maximumLength, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {

            if (maximumLength < _ONE) {
                throw new ArgumentOutOfRangeException("maximumLength", Resources.StringLengthRuleDescriptor_StringLengthRuleDescriptor_must_be_greater_than_0_);
            }

            this.MinimumLength = _MINUS_ONE;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public StringLengthRuleDescriptor(StringLengthValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.AllowNullString = e.AllowNullString;

            if (e.MaximumLength < _ONE) {
                throw new ArgumentOutOfRangeException("e.MaximumLength", Resources.StringLengthRuleDescriptor_StringLengthRuleDescriptor_must_be_greater_than_0_);

            }

            if (e.MaximumLength < e.MinimumLength) {
                throw new ArgumentOutOfRangeException("e.MaximumLength", Resources.StringLengthRuleDescriptor_StringLengthRuleDescriptor_must_be_greater_than_or_equal_to_the_MinimumLength_);
            }

            this.MaximumLength = e.MaximumLength;
            this.MinimumLength = e.MinimumLength;
        }

        #endregion
    }
}

