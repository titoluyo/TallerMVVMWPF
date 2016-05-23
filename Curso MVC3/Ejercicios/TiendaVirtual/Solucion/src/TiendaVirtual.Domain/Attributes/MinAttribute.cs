using System.ComponentModel.DataAnnotations;
namespace TiendaVirtual.Domain.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MinAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly double minValue;

        public MinAttribute(double minValue)
        {
            this.minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            var doubleValue = Convert.ToDouble(value);
            return doubleValue >= minValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = this.FormatErrorMessage(metadata.DisplayName);
            rule.ValidationType = "min";
            rule.ValidationParameters.Add("value", minValue);
            yield return rule;
        }
    }
}