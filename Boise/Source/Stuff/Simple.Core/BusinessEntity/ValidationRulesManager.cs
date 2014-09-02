
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Core.BusinessEntity {

    /// <summary>
    /// The ValidationRulesManager stores all <see cref="ValidationRule" /> for a type.  It's created and managed by the <see cref="SharedValidationRules" /> type.
    /// </summary>
    public class ValidationRulesManager {

        #region Declarations

        List<ValidationRule> _validationRules;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesManager" /> class.
        /// </summary>
        internal ValidationRulesManager() {
            _validationRules = new List<ValidationRule>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified <see cref="ValidationRule" /> to this <see cref="ValidationRulesManager" />.
        /// </summary>
        /// <param name="validationRule">The validation rule.</param>
        public void Add(ValidationRule validationRule) {
            _validationRules.Add(validationRule);
        }

        /// <summary>
        /// Gets all property names with one or more rules.
        /// </summary>
        /// <returns>IEnumerable&lt;String&gt; of all property names that have rules applied to them in this type.</returns>
        public IEnumerable<String> GetAllPropertyNamesWithRules() {
            return _validationRules.Select(r => r.PropertyName).ToList().Distinct();
        }

        /// <summary>
        /// Gets all rules.
        /// </summary>
        /// <returns>IEnumerable&lt;<see cref="ValidationRule" />&gt; of all rules applied to this type.</returns>
        public IEnumerable<ValidationRule> GetAllRules() {
            return _validationRules;
        }

        /// <summary>
        /// Gets the rules for property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns an IEnumerable&lt;<see cref="ValidationRule" />&gt; of all rules applied to the property in this type.</returns>
        public IEnumerable<ValidationRule> GetRulesForProperty(String propertyName) {
            return _validationRules.Where(r => r.PropertyName == propertyName);
        }

        #endregion
    }
}
