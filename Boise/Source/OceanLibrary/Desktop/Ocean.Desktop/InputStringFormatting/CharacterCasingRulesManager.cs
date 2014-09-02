using System;
using System.Collections.Generic;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents CharacterCasingRulesManager, maintains character casing rules for a business Object.
    /// </summary>
    public class CharacterCasingRulesManager {

        #region  Declarations

        Dictionary<String, CharacterCasing> _characterCasingRulesList;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        /// <value>The rules dictionary.</value>
        public Dictionary<String, CharacterCasing> RulesDictionary {
            get { return _characterCasingRulesList ?? (_characterCasingRulesList = new Dictionary<String, CharacterCasing>()); }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingRulesManager"/> class.
        /// </summary>
        public CharacterCasingRulesManager() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Adds a CharacterCasing Formatting rule to the list of rules to be executed when the property is changed.
        /// </summary>
        public void AddRule(String propertyName, CharacterCasing characterCasing) {
            this.RulesDictionary.Add(propertyName, characterCasing);
        }

        /// <summary>
        /// Returns the CharacterCasing rule for a property.
        /// </summary>
        public CharacterCasing GetRuleForProperty(String propertyName) {
            if (RulesDictionary.ContainsKey(propertyName)) {
                return RulesDictionary[propertyName];

            }
            return CharacterCasing.None;
        }

        #endregion
    }
}