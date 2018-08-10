using MVCValidation.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCValidation.Validation
{
    public class RequiredIfOtherFieldIsValue : ValidationAttribute
    {
        public RequiredIfOtherFieldIsValue(string otherProperty, string otherPropertyCheckTriggerValue)
        {
            this.otherProperty = otherProperty;
            this.otherPropertyCheckTriggerValue = otherPropertyCheckTriggerValue;
        }

        public string otherProperty;
        public string otherPropertyCheckTriggerValue;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is DateTime))
            {
                if ((DateTime)value != DateTime.MinValue)
                {
                    return ValidationResult.Success;
                }
            }
            else if ((value != null) && (!string.IsNullOrWhiteSpace(value.ToString())))
            {
                return ValidationResult.Success;
            }
            var model = validationContext.ObjectInstance;
            var otherPropertyValue = GetProperty.Value(model, otherProperty);
            if (otherPropertyValue == null)
            {
                return ValidationResult.Success;
            }

            if (otherPropertyValue.ToString() == otherPropertyCheckTriggerValue)
            {
                return new ValidationResult($"The {GetDisplayNameAttribute.Value(model, validationContext.MemberName)} field is required.");
            }
            return ValidationResult.Success;
        }
    }
}