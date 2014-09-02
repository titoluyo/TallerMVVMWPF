
using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents RuleDescriptorBase
    /// </summary>
    public abstract class RuleDescriptorBase {

        #region  Declarations

        String _brokenRuleDescription = String.Empty;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the broken rule description.
        /// </summary>
        /// <value>The broken rule description.</value>
        public String BrokenRuleDescription {
            get {
                return String.IsNullOrEmpty(_brokenRuleDescription) ? String.Format(Resources.RuleDescriptorBase_BrokenRuleDescription_Missing_Broken_Rule_Description_For_FormatString, this.PropertyName) : _brokenRuleDescription;
            }
            set {
                _brokenRuleDescription = value;
            }
        }

        /// <summary>
        /// Gets or sets the custom message.
        /// </summary>
        /// <value>The custom message.</value>
        public String CustomMessage { get; set; }

        /// <summary>
        /// Gets or sets the override message.
        /// </summary>
        /// <value>The override message.</value>
        public String OverrideMessage { get; set; }

        /// <summary>
        /// Friendly name of property for error reporting purposes.  If not provided, this will be generated from the property name by parsing the property name.
        /// </summary>
        /// <value>The name of the property friendly.</value>
        public String PropertyFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the rule set.
        /// </summary>
        /// <value>The rule set.</value>
        public String RuleSet { get; set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleDescriptorBase"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="overrideMessage">The override message.</param>
        protected RuleDescriptorBase(String propertyName, String propertyFriendlyName, String ruleSet, String customMessage, String overrideMessage) {
            this.PropertyName = propertyName;
            this.PropertyFriendlyName = propertyFriendlyName;
            this.RuleSet = ruleSet;
            this.CustomMessage = customMessage;
            this.OverrideMessage = overrideMessage;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the name of the property friendly.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static String GetPropertyFriendlyName(RuleDescriptorBase e) {
            return String.IsNullOrEmpty(e.PropertyFriendlyName) ? InputStringFormatting.CamelCaseString.GetWords(e.PropertyName) : e.PropertyFriendlyName;
        }

        #endregion
    }
}