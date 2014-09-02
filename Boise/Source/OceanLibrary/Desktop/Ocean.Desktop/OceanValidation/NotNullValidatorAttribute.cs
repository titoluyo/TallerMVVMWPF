using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents NotNullValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NotNullValidatorAttribute : BaseValidatorAttribute {

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidatorAttribute"/> class.
        /// </summary>
        public NotNullValidatorAttribute() {
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(ComparisonValidationRules.NotNullRule, new NotNullRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}