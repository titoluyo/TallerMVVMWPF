using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents DomainRuleDescriptor
    /// </summary>
    /// 
    public class DomainRuleDescriptor : RuleDescriptorBase {

        #region  Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public String[] Data { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        [CLSCompliant(false)]
        public DomainRuleDescriptor(DomainValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.Data = e.Data;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRuleDescriptor"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        /// <param name="data">The data.</param>
        public DomainRuleDescriptor(RequiredEntry requiredEntry, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage, params String[] data)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            this.Data = data;
            this.RequiredEntry = requiredEntry;
        }

        #endregion
    }
}