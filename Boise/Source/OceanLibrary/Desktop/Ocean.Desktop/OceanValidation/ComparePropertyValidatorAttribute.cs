using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ComparePropertyValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ComparePropertyValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; private set; }
        
        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; private set; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparePropertyValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public ComparePropertyValidatorAttribute(ComparisonType comparisonType, String compareToPropertyName, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToPropertyName = compareToPropertyName;
            this.RequiredEntry = requiredEntry;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(ComparisonValidationRules.ComparePropertyRule, new ComparePropertyRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}