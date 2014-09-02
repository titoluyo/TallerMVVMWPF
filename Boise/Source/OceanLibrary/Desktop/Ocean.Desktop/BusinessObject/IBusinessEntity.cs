
using System;

namespace Ocean.BusinessObject {

    /// <summary>
    /// Provides contact for IBusinessEntity classes
    /// </summary>
    public interface IBusinessEntity {
        /// <summary>
        /// Checks all rules.
        /// </summary>
        void CheckAllRules();

        /// <summary>
        /// Gets or sets the active rule set.
        /// </summary>
        /// <value>The active rule set.</value>
        String ActiveRuleSet { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has been validated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has been validated; otherwise, <c>false</c>.
        /// </value>
        Boolean HasBeenValidated { get; }
    }
}

