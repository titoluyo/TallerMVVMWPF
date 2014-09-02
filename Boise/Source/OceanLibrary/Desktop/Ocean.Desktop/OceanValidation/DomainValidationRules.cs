using System;
using System.Linq;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents DomainValidationRules
    /// </summary>
    public class DomainValidationRules {

        #region  Properties

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidationRules"/> class.
        /// </summary>
        public DomainValidationRules() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Domains the rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean DomainRule(Object target, RuleDescriptorBase e) {

            var args = e as DomainRuleDescriptor;

            if(args == null) {
                throw new ArgumentException(String.Format(Resources.DomainValidationRules_DomainRule_Wrong_rule_passed_to_DomainRule___0_FormatString, e.GetType()));
            }

            System.Reflection.PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            String propertyValue = Convert.ToString(objPi.GetValue(target, null));

            if(args.RequiredEntry == RequiredEntry.No && String.IsNullOrEmpty(propertyValue)) {
                return true;
            }

            if(args.Data.Any(s => String.Compare(s, propertyValue, StringComparison.OrdinalIgnoreCase) == 0)) {
                return true;
            }

            var sb = new System.Text.StringBuilder(1024);
            sb.AppendFormat(Resources.DomainValidationRules_DomainRule_The__0__did_not_match_any_of_the_following_acceptable_values_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

            const String format = ", {0}";

            foreach(String s in args.Data) {
                sb.AppendFormat(format, s);
            }

            e.BrokenRuleDescription = sb.ToString();
            return false;
        }

        #endregion
    }
}