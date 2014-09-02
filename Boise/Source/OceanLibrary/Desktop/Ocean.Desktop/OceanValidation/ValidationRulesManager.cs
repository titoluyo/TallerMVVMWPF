using System;
using System.Collections.Generic;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ValidationRulesManager, maintains rule methods for a business Object or business Object type.
    /// </summary>
    public class ValidationRulesManager {

        #region  Declarations

        Dictionary<String, ValidationRulesList> _validationRulesList;

        #endregion

        #region  Properties

        /// <summary>
        /// Returns RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        public Dictionary<String, ValidationRulesList> RulesDictionary {
            get { return _validationRulesList ?? (_validationRulesList = new Dictionary<String, ValidationRulesList>()); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesManager"/> class.
        /// </summary>
        public ValidationRulesManager() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Adds a rule to the list of rules to be enforced.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void AddRule(IValidationRuleMethod rule, String propertyName) {

            List<IValidationRuleMethod> list = GetRulesForProperty(propertyName).List;
            list.Add(rule);
        }

        /// <summary>
        /// Adds a rule to the list of rules to be enforced.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="e">The e.</param>
        /// <param name="ruleType">Type of the rule.</param>
        public void AddRule(RuleHandler handler, RuleDescriptorBase e, RuleType ruleType) {

            List<IValidationRuleMethod> list = GetRulesForProperty(e.PropertyName).List;
            list.Add(new Validator(handler, e, ruleType));
        }

        /// <summary>
        /// Returns the list containing rules for a property. If no list exists one is created and returned.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public ValidationRulesList GetRulesForProperty(String propertyName) {

            if (this.RulesDictionary.ContainsKey(propertyName)) {
                return this.RulesDictionary[propertyName];

            }

            var validationRulesList = new ValidationRulesList();
            this.RulesDictionary.Add(propertyName, validationRulesList);
            return validationRulesList;
        }

        #endregion
    }
}

