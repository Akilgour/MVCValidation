using MVCValidation.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MVCValidation.Validation
{
    public class RequiredIfOtherFieldHasValue : ValidationAttribute
    {
        public RequiredIfOtherFieldHasValue(string otherProperty)
        {
            this.otherProperty = otherProperty;
        }

        public string otherProperty;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance;
            var otherPropertyValue = GetProperty.Value(model, otherProperty);

            if (otherPropertyValue == null)
            {
                return ValidationResult.Success;
            }

            if (value != null)
            {
                return ValidationResult.Success;
            }

            if (!string.IsNullOrEmpty(otherPropertyValue.ToString().Trim()))
            {
                var memberDisplayName = GetDisplayNameAttribute.Value(model, validationContext.MemberName);
                var otherPropertyDisplayName = GetDisplayNameAttribute.Value(model, otherProperty);
                return new ValidationResult($"{memberDisplayName} must have value, when {otherPropertyDisplayName} has value");
            }

            return ValidationResult.Success;
        }
    }
}