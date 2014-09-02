using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ComparePropertyRuleDescriptor
    /// </summary>
    public class ComparePropertyRuleDescriptor : RuleDescriptorBase {

        #region  Properties

        /// <summary>
        /// Gets or sets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public ComparePropertyRuleDescriptor(ComparePropertyValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.ComparisonType = e.ComparisonType;
            this.CompareToPropertyName = e.CompareToPropertyName;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyRuleDescriptor"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public ComparePropertyRuleDescriptor(ComparisonType comparisonType, String compareToPropertyName, RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            this.ComparisonType = comparisonType;
            this.CompareToPropertyName = compareToPropertyName;
            this.RequiredEntry = requiredEntry;
        }

        #endregion
    }
}