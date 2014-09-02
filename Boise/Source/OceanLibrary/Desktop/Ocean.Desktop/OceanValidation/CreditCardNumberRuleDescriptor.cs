using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents CreditCardNumberRuleDescriptor
    /// </summary>
    public class CreditCardNumberRuleDescriptor : RuleDescriptorBase {

        #region  Properties

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardNumberRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public CreditCardNumberRuleDescriptor(CreditCardNumberValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardNumberRuleDescriptor"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public CreditCardNumberRuleDescriptor(RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            this.RequiredEntry = requiredEntry;
        }

        #endregion
    }
}