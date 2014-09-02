using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents IValidationRuleMethod contract
    /// </summary>
    public interface IValidationRuleMethod {

        /// <summary>
        /// Invokes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        Boolean Invoke(Object target);

        /// <summary>
        /// Gets the rule base.
        /// </summary>
        /// <value>The rule base.</value>
        RuleDescriptorBase RuleBase { get; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        String RuleName { get; }
    }
}