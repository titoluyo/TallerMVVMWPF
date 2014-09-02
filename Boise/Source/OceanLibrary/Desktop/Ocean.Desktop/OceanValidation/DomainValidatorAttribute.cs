using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents DomainValidatorAttribute
    /// </summary>
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DomainValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public String[] Data { get; private set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="data">The data.</param>
        public DomainValidatorAttribute(RequiredEntry requiredEntry, params String[] data) {
            this.RequiredEntry = requiredEntry;
            this.Data = new String[data.Length];
            data.CopyTo(this.Data, 0);
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(DomainValidationRules.DomainRule, new DomainRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}