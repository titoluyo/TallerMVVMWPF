using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents NotNullRuleDescriptor
    /// </summary>
    public class NotNullRuleDescriptor : RuleDescriptorBase {

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public NotNullRuleDescriptor(NotNullValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullRuleDescriptor"/> class.
        /// </summary>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public NotNullRuleDescriptor(String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
        }

        #endregion
    }
}