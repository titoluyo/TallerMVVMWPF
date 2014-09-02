using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents StateAbbreviationValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class StateAbbreviationValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAbbreviationValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        public StateAbbreviationValidatorAttribute(RequiredEntry requiredEntry) {
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
            return new Validator(StringValidationRules.StateAbbreviationRule, new StateAbbreviationRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}

