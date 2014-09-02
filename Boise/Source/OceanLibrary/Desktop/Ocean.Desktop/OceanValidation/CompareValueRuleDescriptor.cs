using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents CompareValueRuleDescriptor
    /// </summary>
    public class CompareValueRuleDescriptor : RuleDescriptorBase {

       #region  Properties

        /// <summary>
        /// Gets or sets the compare to value.
        /// </summary>
        /// <value>The compare to value.</value>
        public IComparable CompareToValue { get; set; }

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
        /// Initializes a new instance of the <see cref="CompareValueRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public CompareValueRuleDescriptor(CompareValueValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.ComparisonType = e.ComparisonType;
            this.CompareToValue = e.CompareToValue;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueRuleDescriptor"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public CompareValueRuleDescriptor(ComparisonType comparisonType, RequiredEntry requiredEntry, IComparable compareToValue, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        #endregion
    }
}