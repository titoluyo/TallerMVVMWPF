using System;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ComparisonValidationRules
    /// </summary>
    public class ComparisonValidationRules {

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonValidationRules"/> class.
        /// </summary>
        public ComparisonValidationRules() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Compares the property rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean ComparePropertyRule(Object target, RuleDescriptorBase e) {

            var args = e as ComparePropertyRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.ComparisonValidationRules_ComparePropertyRule_Wrong_rule_passed_to_ComparePropertyRule_FormatString, e.GetType()));
            }

            System.Reflection.PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            Object source = objPi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if (source == null || Convert.IsDBNull(source) || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__was_null_or_empty_but_is_a_required_field_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {
                if (source == null || Convert.IsDBNull(source)) {
                    return true;
                }
            }

            objPi = target.GetType().GetProperty(args.CompareToPropertyName);

            Object testAgainst = objPi.GetValue(target, null);

            if (testAgainst == null || Convert.IsDBNull(testAgainst)) {
                return true;
            }

            var iSource = (IComparable)source;
            var iTestAgainst = (IComparable)testAgainst;
            Int32 result = iSource.CompareTo(iTestAgainst);

            switch (args.ComparisonType) {

                case ComparisonType.Equal:

                    if (result == 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.GreaterThan:

                    if (result > 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_greater_than__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.GreaterThanEqual:

                    if (result >= 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_greater_than_or_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.LessThan:

                    if (result < 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_less_than__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.LessThanEqual:

                    if (result <= 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_less_than_or_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.NotEqual:

                    if (result != 0) {
                        return true;
                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_not_equal__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                default:
                    throw new OverflowException(Resources.ComparisonValidationRules_ComparePropertyRule_Comparision_type_not_programmed);
            }

        }

        /// <summary>
        /// Compares the value rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean CompareValueRule(Object target, RuleDescriptorBase e) {

            var args = e as CompareValueRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.ComparisonValidationRules_CompareValueRule_Wrong_rule_passed_to_CompareValueRule___0_FormatString, e.GetType()));
            }

            System.Reflection.PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            Object source = objPi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if(source == null || Convert.IsDBNull(source) || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__was_null_or_empty_but_is_a_required_field_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (source == null || Convert.IsDBNull(source)) {
                    return true;
                }

            }

            Object testAgainst = args.CompareToValue;
            var iSource = (IComparable)source;
            Int32 result = iSource.CompareTo(args.CompareToValue);

            switch (args.ComparisonType) {

                case ComparisonType.Equal:

                    if (result == 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.GreaterThan:

                    if (result > 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_greater_than__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.GreaterThanEqual:

                    if (result >= 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_greater_than_or_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.LessThan:

                    if (result < 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_less_than__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.LessThanEqual:

                    if (result <= 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_be_less_than_or_equal_to__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                case ComparisonType.NotEqual:

                    if (result != 0) {
                        return true;

                    }
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__must_not_equal__1__FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), testAgainst);
                    return false;

                default:
                    throw new OverflowException(Resources.ComparisonValidationRules_ComparePropertyRule_Comparision_type_not_programmed);
            }

        }

        /// <summary>
        /// Ins the range rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean InRangeRule(Object target, RuleDescriptorBase e) {

            var args = e as RangeRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.ComparisonValidationRules_InRangeRule_Wrong_rule_passed_to_InRangeRule___0_FormatString, e.GetType()));
            }

            System.Reflection.PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            Object source = objPi.GetValue(target, null);
            var stringSource = source as String;

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if(source == null || Convert.IsDBNull(source) || (stringSource != null && String.IsNullOrWhiteSpace(stringSource))) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_ComparePropertyRule__0__was_null_or_empty_but_is_a_required_field_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (source == null || Convert.IsDBNull(source)) {
                    return true;
                }
            }

            var iSource = (IComparable)source;
            Object lower = args.LowerValue;
            Object upper = args.UpperValue;
            Int32 lowerResult = iSource.CompareTo(args.LowerValue);

            if (args.LowerRangeBoundaryType == RangeBoundaryType.Inclusive) {

                if (lowerResult < 0) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_InRangeRule__0__must_be_greater_than_or_equal_to__1_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), lower);
                    return false;
                }

            } else {

                if (lowerResult <= 0) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_InRangeRule__0__must_be_greater_than__1_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), lower);
                    return false;
                }

            }

            Int32 upperResult = iSource.CompareTo(args.UpperValue);

            if (args.UpperRangeBoundaryType == RangeBoundaryType.Inclusive) {

                if (upperResult > 0) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_InRangeRule__0__must_be_less_than__1_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), upper);
                    return false;
                }

            } else {

                if (upperResult >= 0) {
                    e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_InRangeRule__0__must_be_less_than_or_equal_to__1_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), upper);
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Nots the null rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean NotNullRule(Object target, RuleDescriptorBase e) {

            //Boxing and Unboxing
            //When a nullable type is boxed, the common language runtime automatically boxes the underlying value of the 
            //Nullable Object, not the Nullable Object itself. That is, if the HasValue property is true, the contents 
            //of the Value property is boxed. If the HasValue property is false, a null reference (Nothing in Visual Basic) is boxed. 
            //When the underlying value of a nullable type is unboxed, the common language runtime creates a new 
            //Nullable structure initialized to the underlying value. 
            var args = e as NotNullRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.ComparisonValidationRules_NotNullRule_Wrong_rule_passed_to_NotNullRule___0_FormatString, e.GetType()));
            }

            System.Reflection.PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            Object source = objPi.GetValue(target, null);

            //this handles both Nullable and standard uninitialized values
            if (source == null) {
                e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_NotNullRule__0__is_null_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                return false;

            }
 
            if (Convert.IsDBNull(source)) {
                e.BrokenRuleDescription = String.Format(Resources.ComparisonValidationRules_NotNullRule__0__is_DBNull_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                return false;
            }

            return true;
        }
 
        #endregion
    }
}