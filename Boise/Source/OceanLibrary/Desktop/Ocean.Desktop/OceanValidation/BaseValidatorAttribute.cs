
using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents BaseValidatorAttribute
    /// </summary> 
    public abstract class BaseValidatorAttribute : Attribute {

        #region  Properties

        /// <summary>
        /// Get and sets CustomMessage. Developers can specify a custom message that is displayed when the validation rule is broken.  The custom message is displayed first and the normal broken rule is appended to the custom message if provided.
        /// </summary>
        /// <value>The custom message.</value>
        public String CustomMessage {get; set;}

        /// <summary>
        /// Get and sets OverrideMessage. Developers can specify an override message that is displayed when the validation rule is broken.  If supplied, override message replaces the normal broken rule or custom message.
        /// </summary>
        /// <value>The override message.</value>
        public String OverrideMessage {get; set;}

        /// <summary>
        /// Gets and sets PropertyFriendlyName. When broken rule messages are constructed, the Object property name is parsed and a friendly name is created.  If the objec property name would not make a good user friendly property name, then developers can add a PropertyFriendlyName that will be used for constructing broken rule messages.
        /// </summary>
        /// <value>The name of the property friendly.</value>
        public String PropertyFriendlyName {get; set;}

        /// <summary>
        /// Gets and sets RuleSet. If the RuleSet property is not specified then that rule will always be checked.  If the RuleSet is specified then that rule will only be validated when that RuleSet is active.  If a rule applies to more than one RuleSet, then enter each RuleSet name separated by the pipe symbol.  Example:  RuleSet:="Update|Delete"
        /// </summary>
        public String RuleSet {get; set;}

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValidatorAttribute"/> class.
        /// </summary>
        protected BaseValidatorAttribute() {
            this.CustomMessage = String.Empty;
            this.OverrideMessage = String.Empty;
            this.PropertyFriendlyName = String.Empty;
            this.RuleSet = String.Empty;
        }

        #region  Abstract Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public abstract Validator Create(String propertyName);

        #endregion
    }
}