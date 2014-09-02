using System;
using System.Collections.Generic;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents SharedCharacterCasingRules
    /// </summary>
    public class SharedCharacterCasingRules {

        #region  Declarations

        static readonly Dictionary<Type, CharacterCasingRulesManager> _CharacterCasingRulesManagers = new Dictionary<Type, CharacterCasingRulesManager>();

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedCharacterCasingRules"/> class.
        /// </summary>
        SharedCharacterCasingRules() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the <see cref="CharacterCasingRulesManager"/> for the specified Object type, optionally creating a new instance of the Object if necessary.
        /// </summary>
        /// <param name="type">
        /// Type of business Object for which the rules apply.
        /// </param>
        public static CharacterCasingRulesManager GetManager(Type type) {
            lock (_CharacterCasingRulesManagers) {
                CharacterCasingRulesManager manager;
                if (!(_CharacterCasingRulesManagers.TryGetValue(type, out manager))) {
                    manager = new CharacterCasingRulesManager();
                    _CharacterCasingRulesManagers.Add(type, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Returns a Boolean value indicating whether a set of rules have been created for a given <see cref="Type" />.
        /// </summary>
        /// <param name="type">
        /// Type of business Object for which the rules apply.
        /// </param>
        /// <returns><see langword="true" /> if rules exist for the type.</returns>
        public static Boolean RulesExistFor(Type type) {
            return _CharacterCasingRulesManagers.ContainsKey(type);
        }

        #endregion
    }
}