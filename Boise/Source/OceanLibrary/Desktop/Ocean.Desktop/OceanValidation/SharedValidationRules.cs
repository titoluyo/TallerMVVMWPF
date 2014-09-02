using System;
using System.Collections.Generic;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents SharedValidationRules, maintains a list of all the per-type <see cref="ValidationRulesManager"/> objects loaded in memory.
    /// </summary>
    public static class SharedValidationRules {

        #region  Declarations

        static readonly Dictionary<Type, ValidationRulesManager> _ValidationRuleManagers = new Dictionary<Type, ValidationRulesManager>();

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the <see cref="ValidationRulesManager"/> for the specified Object type, optionally creating a new instance of the Object if necessary.
        /// </summary>
        /// <param name="type">
        /// Type of business Object for which the rules apply.
        /// </param>
        public static ValidationRulesManager GetManager(Type type) {

            lock (_ValidationRuleManagers) {
                ValidationRulesManager manager;
                if (!(_ValidationRuleManagers.TryGetValue(type, out manager))) {
                    manager = new ValidationRulesManager();
                    _ValidationRuleManagers.Add(type, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a set of rules have been created for a given <see cref="Type" />.
        /// </summary>
        /// <param name="type">
        /// Type of business Object for which the rules apply.
        /// </param>
        /// <returns><see langword="true" /> if rules exist for the type.</returns>
        public static Boolean RulesExistFor(Type type) {
            return _ValidationRuleManagers.ContainsKey(type);
        }

        #endregion
    }
}

