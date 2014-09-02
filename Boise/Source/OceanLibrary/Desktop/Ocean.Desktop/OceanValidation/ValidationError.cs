using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ValidationError
    /// </summary>
    public class ValidationError {

        #region  Properties

        /// <summary>
        /// Gets or sets the broken rule.
        /// </summary>
        /// <value>The broken rule.</value>
        public IValidationRuleMethod BrokenRule { get; private set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName {
            get {
                return this.BrokenRule.RuleBase.PropertyName;
            }
        }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public String RuleName {
            get {
                return this.BrokenRule.RuleName;
            }
        }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="brokenRule">The broken rule.</param>
        public ValidationError(IValidationRuleMethod brokenRule) {
            this.BrokenRule = brokenRule;
        }

        #endregion
    }
}

