using System;
using System.Windows.Controls;

namespace CookMe.Common.Infrastructure {

    public class RequiredEntryValidationRule : ValidationRule {

        public String FieldTag { get; set; }

        public override ValidationResult Validate(Object value, System.Globalization.CultureInfo cultureInfo) {
           if (value == null || (value is String && String.IsNullOrWhiteSpace((String)value))) {
                return String.IsNullOrWhiteSpace(this.FieldTag) ? 
                    new ValidationResult(false, "this field requires an entry.") : 
                    new ValidationResult(false, String.Concat(this.FieldTag, " is a required field."));
            }
            return ValidationResult.ValidResult;
        }
    }
}
