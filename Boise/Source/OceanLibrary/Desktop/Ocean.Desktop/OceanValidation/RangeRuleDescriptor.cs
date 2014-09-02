using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents RangeRuleDescriptor
    /// </summary>
    public class RangeRuleDescriptor : RuleDescriptorBase {

        #region  Properties

        /// <summary>
        /// Gets or sets the type of the lower range boundary.
        /// </summary>
        /// <value>The type of the lower range boundary.</value>
        public RangeBoundaryType LowerRangeBoundaryType { get; set; }

        /// <summary>
        /// Gets or sets the lower value.
        /// </summary>
        /// <value>The lower value.</value>
        public IComparable LowerValue { get; set; }

        /// <summary>
        /// Gets or sets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; }

        /// <summary>
        /// Gets or sets the type of the upper range boundary.
        /// </summary>
        /// <value>The type of the upper range boundary.</value>
        public RangeBoundaryType UpperRangeBoundaryType { get; set; }

        /// <summary>
        /// Gets or sets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public IComparable UpperValue { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRuleDescriptor"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyName">Name of the property.</param>
        public RangeRuleDescriptor(RangeValidatorAttribute e, String propertyName)
            : base(propertyName, e.PropertyFriendlyName, e.RuleSet, e.CustomMessage, e.OverrideMessage) {
            this.LowerRangeBoundaryType = e.LowerRangeBoundaryType;
            this.UpperRangeBoundaryType = e.UpperRangeBoundaryType;
            this.LowerValue = e.LowerValue;
            this.UpperValue = e.UpperValue;
            this.RequiredEntry = e.RequiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRuleDescriptor"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="customMessage">The custom message.</param>
        /// <param name="propertyFriendlyName">Name of the property friendly.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="overrideMessage">The override message.</param>
        public RangeRuleDescriptor(RangeBoundaryType lowerRangeBoundaryType, RequiredEntry requiredEntry, RangeBoundaryType upperRangeBoundaryType, IComparable lowerValue, IComparable upperValue, String customMessage, String propertyFriendlyName, String propertyName, String ruleSet, String overrideMessage)
            : base(propertyName, propertyFriendlyName, ruleSet, customMessage, overrideMessage) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperValue = upperValue;
        }

        #endregion
    }
}