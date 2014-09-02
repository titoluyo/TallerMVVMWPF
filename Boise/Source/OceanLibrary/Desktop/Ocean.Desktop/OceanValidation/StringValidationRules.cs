using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents StringValidationRules
    /// </summary>
    public class StringValidationRules {

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValidationRules"/> class.
        /// </summary>
        public StringValidationRules() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Banks the routing number rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean BankRoutingNumberRule(Object target, RuleDescriptorBase e) {

            var args = e as BankRoutingNumberRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.StringValidationRules_BankRoutingNumberRule_Wrong_rule_passed_to_BankRoutingNumberRule_FormatString, e.GetType()));
            }

            PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);

            if (!(objPi.PropertyType == typeof(String))) {
                throw new NotSupportedException(Resources.StringValidationRules_BankRoutingNumberRule_Bank_routing_number_validation_rule_can_only_be_applied_to_String_properties_);
            }

            String bankRoutingNumber = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if (String.IsNullOrEmpty(bankRoutingNumber) || Convert.IsDBNull(bankRoutingNumber)) {
                    e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule__0__was_null_or_empty_but_is_a_required_field_, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (String.IsNullOrEmpty(bankRoutingNumber) || Convert.IsDBNull(bankRoutingNumber)) {
                    return true;
                }
            }

            Int32 lengthBankRoutingNumber = bankRoutingNumber.Length;
            Int32 value = 0;

            if (lengthBankRoutingNumber != 9) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule_The_entered_value__0__is_not_a_valid_bank_routing_number___All_bank_routing_numbers_are_9_digits_in_length_FormatString, bankRoutingNumber);
                return false;
            }

            if (Int32.Parse(bankRoutingNumber.Substring(0, 1)) > 1) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule_The_entered_value__0__is_not_a_valid_bank_routing_number___The_first_digit_must_be_a_0_or_a_1_FormatString, bankRoutingNumber);
                return false;
            }

            if (bankRoutingNumber.Any(c => !(Char.IsDigit(c)))) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule_The_entered_value__0__is_not_a_valid_bank_routing_number___Only_numeric_input_is_allowed_FormatString, bankRoutingNumber);
                return false;
            }

            for (Int32 intX = 0; intX <= 8; intX += 3) {
                value += Int32.Parse(bankRoutingNumber.Substring(intX, 1)) * 3;
                value += Int32.Parse(bankRoutingNumber.Substring(intX + 1, 1)) * 7;
                value += Int32.Parse(bankRoutingNumber.Substring(intX + 2, 1));
            }

            if (value % 10 != 0) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule_The_entered_value__0__is_not_a_valid_bank_routing_number_FormatString, bankRoutingNumber);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Credits the card number rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean CreditCardNumberRule(Object target, RuleDescriptorBase e) {

            var args = e as CreditCardNumberRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.StringValidationRules_CreditCardNumberRule_Wrong_rule_passed_to_CreditCardNumberRule___0_FormatString, e.GetType()));
            }

            PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);

            if (!(objPi.PropertyType == typeof(String))) {
                throw new NotSupportedException(Resources.StringValidationRules_CreditCardNumberRule_Credit_card_number_validation_rule_can_only_be_applied_to_String_properties_);
            }

            String cardNumber = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if (String.IsNullOrEmpty(cardNumber) || Convert.IsDBNull(cardNumber)) {
                    e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule__0__was_null_or_empty_but_is_a_required_field_, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (String.IsNullOrEmpty(cardNumber) || Convert.IsDBNull(cardNumber)) {
                    return true;
                }

            }

            Int32 lengthCreditCardNumber = cardNumber.Length;
            var cardArray = new Int32[lengthCreditCardNumber + 1];
            Int32 value;

            if (cardNumber.Any(c => !(char.IsDigit(c)))) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_CreditCardNumberRule_The_entered_value__0__is_not_a_valid_credit_card_number___Only_numeric_input_is_allowed_FormatString, cardNumber);
                return false;
            }

            for (var count = lengthCreditCardNumber - 1; count >= 1; count -= 2) {
                value = Convert.ToInt32(cardNumber.Substring(count - 1, 1)) * 2;
                cardArray[count] = value;
            }

            value = 0;

            Int32 start = lengthCreditCardNumber % 2 == 0 ? 2 : 1;

            for (var count = start; count <= lengthCreditCardNumber; count += 2) {
                value = value + Convert.ToInt32(cardNumber.Substring(count - 1, 1));
                Int32 arrValue = cardArray[count - 1];

                if (arrValue < 10) {
                    value = value + arrValue;

                } else {
                    value = value + Convert.ToInt32(Convert.ToString(arrValue).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(arrValue).Substring(Convert.ToString(arrValue).Length - 1));
                }

            }

            if (value % 10 != 0) {
                args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_CreditCardNumberRule_The_entered_value__0__is_not_a_valid_credit_card_number_FormatString, cardNumber);
                return false;

            }
            return true;
        }

        /// <summary>
        /// Determines whether [is regular expression pattern valid] [the specified regular expression pattern].
        /// </summary>
        /// <param name="regularExpressionPattern">The regular expression pattern.</param>
        /// <returns>
        /// 	<c>true</c> if [is regular expression pattern valid] [the specified regular expression pattern]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsRegularExpressionPatternValid(String regularExpressionPattern) {

            Boolean returnValue = false;

            try {
                new Regex(regularExpressionPattern);
                returnValue = true;
// ReSharper disable EmptyGeneralCatchClause
            } catch {
// ReSharper restore EmptyGeneralCatchClause
            }

            return returnValue;
        }

        /// <summary>
        /// Regulars the expression rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean RegularExpressionRule(Object target, RuleDescriptorBase e) {

            //great site for patterns
            //http://regexlib.com/Search.aspx
            var args = e as RegularExpressionRuleDescriptor;

            if (args == null) {
                throw new NullReferenceException(String.Format(Resources.StringValidationRules_RegularExpressionRule_Wrong_rule_passed_to_RegularExpressionRule___0_FormatString, e.GetType()));
            }

            PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            String testMe = Convert.ToString(objPi.GetValue(target, null));

            if (args.RegularExpressionPatternType == RegularExpressionPatternType.Custom && String.IsNullOrEmpty(args.CustomRegularExpressionPattern)) {
                throw new InvalidOperationException(Resources.StringValidationRules_RegularExpressionRule_CustomRegularExpressionPattern_not_supplied);
            }

            if (!(IsRegularExpressionPatternValid(args.CustomRegularExpressionPattern))) {
                throw new InvalidOperationException(Resources.StringValidationRules_RegularExpressionRule_CustomRegularExpressionPattern_not_supplied);

            }
 
            if (args.RequiredEntry == RequiredEntry.Yes) {

                if (String.IsNullOrWhiteSpace(testMe) || Convert.IsDBNull(testMe)) {
                    e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule__0__was_null_or_empty_but_is_a_required_field_, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (testMe.Trim().Length == 0) {
                    return true;
                }
            }

            String pattern;
            String brokenRuleDescription;

            switch (args.RegularExpressionPatternType) {

                case RegularExpressionPatternType.Custom:
                    pattern = args.CustomRegularExpressionPattern;
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required__1__pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), pattern);

                    break;
                case RegularExpressionPatternType.Email:
                    pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required_email_pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

                    break;
                case RegularExpressionPatternType.IPAddress:
                    pattern = "^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])$";
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required_IP_Address_pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

                    break;
                case RegularExpressionPatternType.SSN:
                    pattern = "^\\d{3}-\\d{2}-\\d{4}$";
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required_SSN_pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

                    break;
                case RegularExpressionPatternType.Url:
                    pattern = "(?#WebOrIP)((?#protocol)((news|nntp|telnet|http|ftp|https|ftps|sftp):\\/\\/)?(?#subDomain)(([a-zA-Z0-9]+\\.*(?#domain)[a-zA-Z0-9\\-]+(?#TLD)(\\.[a-zA-Z]+){1,2})|(?#IPAddress)((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))+(?#Port)(:[1-9][0-9]*)?)+(?#Path)((\\/((?#dirOrFileName)[a-zA-Z0-9_\\-\\%\\~\\+]+)?)*)?(?#extension)(\\.([a-zA-Z0-9_]+))?(?#parameters)(\\?([a-zA-Z0-9_\\-]+\\=[a-z-A-Z0-9_\\-\\%\\~\\+]+)?(?#additionalParameters)(\\&([a-zA-Z0-9_\\-]+\\=[a-z-A-Z0-9_\\-\\%\\~\\+]+)?)*)?";
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required_URL_pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

                    break;
                case RegularExpressionPatternType.ZipCode:
                    pattern = "^\\d{5}(-\\d{4})?$";
                    brokenRuleDescription = String.Format(Resources.StringValidationRules_RegularExpressionRule__0__did_not_match_the_required_Zip_Code_pattern_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));

                    break;
                default:
                    throw new OverflowException(Resources.StringValidationRules_RegularExpressionRule_Programmer_did_not_program_this_RegularExpressionPatternType);
            }

            if (Regex.IsMatch(testMe, pattern, RegexOptions.IgnoreCase)) {
                return true;

            }
            e.BrokenRuleDescription = brokenRuleDescription;
            return false;
        }

        /// <summary>
        /// States the abbreviation rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean StateAbbreviationRule(Object target, RuleDescriptorBase e) {

            var args = e as StateAbbreviationRuleDescriptor;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.StringValidationRules_StateAbbreviationRule_Wrong_rule_passed_to_StateAbbreviationRule___0_FormatString, e.GetType()));
            }

            PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);

            if (!(objPi.PropertyType == typeof(String))) {
                throw new NotSupportedException(Resources.StringValidationRules_StateAbbreviationRule_State_abbreviation_validation_rule_can_only_be_applied_to_String_properties_FormatString);
            }

            String state = Convert.ToString(objPi.GetValue(target, null));

            if (args.RequiredEntry == RequiredEntry.Yes) {

                if (String.IsNullOrEmpty(state) || Convert.IsDBNull(state)) {
                    e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_BankRoutingNumberRule__0__was_null_or_empty_but_is_a_required_field_, RuleDescriptorBase.GetPropertyFriendlyName(e));
                    return false;
                }

            } else {

                if (String.IsNullOrEmpty(state) || Convert.IsDBNull(state)) {
                    return true;
                }
            }

            if (StateAbbreviationValidator.CreateInstance().IsValid(state)) {
                return true;

            }
            args.BrokenRuleDescription = String.Format(Resources.StringValidationRules_StateAbbreviationRule_The_entered_value__0__is_not_a_valid_state_abbreviation_FormatString, state);
            return false;
        }

        /// <summary>
        /// Strings the length rule.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Boolean StringLengthRule(Object target, RuleDescriptorBase e) {

            var args = (StringLengthRuleDescriptor)e;

            if (args == null) {
                throw new ArgumentException(String.Format(Resources.StringValidationRules_StringLengthRule_Wrong_rule_passed_to_StringLengthRule___0_FormatString, e.GetType()));
            }

            PropertyInfo objPi = target.GetType().GetProperty(args.PropertyName);
            String testMe = Convert.ToString(objPi.GetValue(target, null));

// ReSharper disable ConditionIsAlwaysTrueOrFalse
            if(args.AllowNullString == AllowNullString.No && (testMe == null || Convert.IsDBNull(testMe))) {
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_StringLengthRule__0__can_not_be_null_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e));
                return false;
            }
 
            if (args.AllowNullString == AllowNullString.Yes && (String.IsNullOrWhiteSpace(testMe) || Convert.IsDBNull(testMe))) {
                return true;
            }

            if (Convert.IsDBNull(testMe) || String.IsNullOrEmpty(testMe)) {
                testMe = String.Empty;
            }

            if (args.MinimumLength > 0 && testMe.Length < args.MinimumLength) {
                e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_StringLengthRule__0__can_not_be_less_than__1__character_s__long_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), args.MinimumLength);
                return false;
            }

            if (args.MaximumLength > 0 && testMe.Length > args.MaximumLength) {
                e.BrokenRuleDescription = String.Format(Resources.StringValidationRules_StringLengthRule__0__can_not_be_greater_than__1__character_s__long_FormatString, RuleDescriptorBase.GetPropertyFriendlyName(e), args.MaximumLength);
                return false;
            }

            return true;
        }

        #endregion
    }
}

