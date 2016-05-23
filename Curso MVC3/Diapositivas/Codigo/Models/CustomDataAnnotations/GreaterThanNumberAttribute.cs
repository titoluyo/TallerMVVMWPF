using System;

namespace Models.CustomDataAnnotations
{
    using System.ComponentModel.DataAnnotations;

    public class GreaterThanNumberAttribute : ValidationAttribute
    {
        private readonly string otherPropertyName;
        public GreaterThanNumberAttribute(string otherPropertyName): base("{0} must be greater than {1}"){
            this.otherPropertyName = otherPropertyName;
        }

        public override string FormatErrorMessage(string name){
            return String.Format(ErrorMessageString, name, otherPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(otherPropertyName);
            var otherNumber = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var thisNumber = (int)value;
            if (thisNumber <= otherNumber) 
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            return null;
        }
    }
}