
using System;
using System.Collections.Generic;

namespace Simple.Core.BusinessEntity {

    /// <summary>
    /// Maintains a list of all the per-type <see cref="ValidationRulesManager"/> objects loaded in memory.
    /// </summary>
    public static class SharedValidationRules {

        #region Declarations

        static Object _lockObject = new Object();
        static Dictionary<Type, ValidationRulesManager> _validationRuleManagers = new Dictionary<Type, ValidationRulesManager>();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="ValidationRulesManager"/> for the specified object <see cref="Type" />.  Will create a new <see cref="ValidationRulesManager"/> if required.
        /// </summary>
        /// <param name="type"><see cref="Type" /> of business object for which the rules apply.</param>
        /// <returns><see cref="ValidationRulesManager"/> for this <see cref="Type" /></returns>
        public static ValidationRulesManager GetManager(Type type) {
            ValidationRulesManager manager = null;
            lock (_lockObject) {

                if (!_validationRuleManagers.TryGetValue(type, out manager)) {
                    manager = new ValidationRulesManager();
                    _validationRuleManagers.Add(type, manager);
                }
                return manager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a set of rules have been created for a given <see cref="Type" />.
        /// </summary>
        /// <param name="type"><see cref="Type" /> of business object for which the rules apply.</param>
        /// <returns><see langword="true" /> if rules exist for the <see cref="Type" />.</returns>
        public static Boolean RulesExistFor(Type type) {
            return _validationRuleManagers.ContainsKey(type);
        }

        #endregion
    }
}
