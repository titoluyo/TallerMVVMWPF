using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents RangeValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RangeValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets the type of the lower range boundary.
        /// </summary>
        /// <value>The type of the lower range boundary.</value>
        public RangeBoundaryType LowerRangeBoundaryType { get; private set; }

        /// <summary>
        /// Gets the lower value.
        /// </summary>
        /// <value>The lower value.</value>
        public IComparable LowerValue { get; private set; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        /// <summary>
        /// Gets the type of the upper range boundary.
        /// </summary>
        /// <value>The type of the upper range boundary.</value>
        public RangeBoundaryType UpperRangeBoundaryType { get; private set; }

        /// <summary>
        /// Gets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public IComparable UpperValue { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Double lowerValue, RangeBoundaryType upperRangeBoundaryType, Double upperValue, RequiredEntry requiredEntry) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int32 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int32 upperValue, RequiredEntry requiredEntry) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int64 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int64 upperValue, RequiredEntry requiredEntry) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int16 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int16 upperValue, RequiredEntry requiredEntry) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Single lowerValue, RangeBoundaryType upperRangeBoundaryType, Single upperValue, RequiredEntry requiredEntry) {
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, RequiredEntry requiredEntry) {
            this.LowerValue = lowerValue;
            this.UpperValue = upperValue;
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// HACK - New Feature - fixed constructor to support date and decimal types since these are not allowed in a attributes constructor
        /// There is some restriction on the data types that can be used as attribute parameters.
        /// Parameters can be any integral data type (Byte, Short, Integer, Long) or floating point data type (Single and Double), as well as Char, String, Boolean, an enumerated type, or System.Type.
        /// Thus, Date, Decimal, Object, and structured types cannot be used as parameters.
        /// </summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, RequiredEntry requiredEntry, ConvertToType convertToType) {

            switch (convertToType) {

                case ConvertToType.Date:
                    this.LowerValue = Convert.ToDateTime(lowerValue);
                    this.UpperValue = Convert.ToDateTime(upperValue);

                    break;
                case ConvertToType.Decimal:
                    this.LowerValue = Convert.ToDecimal(lowerValue);
                    this.UpperValue = Convert.ToDecimal(upperValue);

                    break;
                default:
                    throw new OverflowException(String.Format(Resources.RangeValidatorAttribute_RangeValidatorAttribute_This_ConvertToType_has_not_yet_been_programmed__value_passed_was___0_FormatString, convertToType));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(ComparisonValidationRules.InRangeRule, new RangeRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}