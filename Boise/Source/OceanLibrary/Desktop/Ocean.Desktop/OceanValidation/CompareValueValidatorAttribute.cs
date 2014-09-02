using System;
using Ocean.Properties;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents CompareValueValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CompareValueValidatorAttribute : BaseValidatorAttribute {

        #region  Properties

        /// <summary>
        /// Gets the compare to value.
        /// </summary>
        /// <value>The compare to value.</value>
        public IComparable CompareToValue { get; private set; }

        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; private set; }

        /// <summary>
        /// Gets the required entry.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Double compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int32 compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int64 compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int16 compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Single compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, RequiredEntry requiredEntry) {
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// HACK - New Feature - fixed constructor to support date and decimal types since these are not allowed in a attributes constructor
        /// There is some restriction on the data types that can be used as attribute parameters.
        /// Parameters can be any integral data type (Byte, Short, Integer, Long) or floating point data type (Single and Double), as well as Char, String, Boolean, an enumerated type, or System.Type.
        /// Thus, Date, Decimal, Object, and structured types cannot be used as parameters.
        /// </summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, RequiredEntry requiredEntry, ConvertToType convertToType) {
            this.ComparisonType = comparisonType;

            switch (convertToType) {

                case ConvertToType.Date:
                  this.CompareToValue = Convert.ToDateTime(compareToValue);

                    break;
                case ConvertToType.Decimal:
                    this.CompareToValue = Convert.ToDecimal(compareToValue);

                    break;
                default:
                    throw new OverflowException(String.Format(Resources.CompareValueValidatorAttribute_CompareValueValidatorAttribute_This_ConvertToType_has_not_yet_been_programmed__value_passed_was___0_FomatString, convertToType));
            }

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
            return new Validator(ComparisonValidationRules.CompareValueRule, new CompareValueRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}