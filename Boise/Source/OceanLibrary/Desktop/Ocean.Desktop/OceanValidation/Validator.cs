using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents Validator
    /// </summary>
    public class Validator : IValidationRuleMethod {
        
        #region  Declarations

        const String _INSTANCE = "instance";
        const String _RULE_FORMATSTRING = "rule://{0}/{1}-{2}";
        readonly RuleHandler _ruleHandler;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the rule base.
        /// </summary>
        /// <value>The rule base.</value>
        public RuleDescriptorBase RuleBase { get; private set; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public String RuleName { get; private set; }

        /// <summary>
        /// Gets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public RuleType RuleType { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Creates a shared or instance validator
        /// </summary>
        public Validator(RuleHandler ruleHandler, RuleDescriptorBase e, RuleType ruleType) {
            _ruleHandler = ruleHandler;
            this.RuleBase = e;
            this.RuleType = ruleType;

            switch (ruleType) {

                case RuleType.Instance:
                    this.RuleName = _INSTANCE + this.RuleName;

                    break;
                case RuleType.Shared:
                    break;
                case RuleType.Attribute:
                    this.RuleName = String.Format(_RULE_FORMATSTRING, ruleHandler.Method.Name, e.PropertyName, (Guid.NewGuid()));

                    break;
                default:
                    throw new ArgumentException(Resources.Validator_Validator_RuleType_not_programmed_yet, "RuleType");
            }
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Invokes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public Boolean Invoke(Object target) {
            return _ruleHandler.Invoke(target, this.RuleBase);
        }

        #endregion
    }
}