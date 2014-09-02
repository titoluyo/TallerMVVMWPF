using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents StringLengthValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class StringLengthValidatorAttribute : BaseValidatorAttribute {

        #region Declarations

        const Int32 _MINUS_ONE = -1;
        const Int32 _ONE = 1;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the allow null String.
        /// </summary>
        /// <value>The allow null String.</value>
        public AllowNullString AllowNullString { get; private set; }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; private set; }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; private set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        public StringLengthValidatorAttribute(Int32 maximumLength)
            : this(_MINUS_ONE, maximumLength, AllowNullString.No) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength)
            : this(minimumLength, maximumLength, AllowNullString.No) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">The allow null String.</param>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength, AllowNullString allowNullString) {

            if (maximumLength < _ONE) {
                throw new ArgumentOutOfRangeException("maximumLength", Resources.StringLengthValidatorAttribute_StringLengthValidatorAttribute_must_be_greater_than_0_);

            }
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException("maximumLength", Resources.StringLengthValidatorAttribute_StringLengthValidatorAttribute_must_be_greater_than_or_equal_to_the_MinimumLength_);
            }

            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">The allow null String.</param>
        public StringLengthValidatorAttribute(Int32 maximumLength, AllowNullString allowNullString)
            : this(_MINUS_ONE, maximumLength, allowNullString) {
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the validator for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public override Validator Create(String propertyName) {
            return new Validator(StringValidationRules.StringLengthRule, new StringLengthRuleDescriptor(this, propertyName), RuleType.Attribute);
        }

        #endregion
    }
}

