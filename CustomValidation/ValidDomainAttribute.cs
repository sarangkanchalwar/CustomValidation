using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomValidation
{
    public class ValidDomainAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var domain = value.ToString();
            if (!domain.Contains('.'))
                return new ValidationResult("Invalid domain name.");

            var charCount = Regex.Matches(domain, @"[a-z]").Count;
            var periodCount = Regex.Matches(domain, @"[.]").Count;
            if (domain.Length != (charCount + periodCount))
                return new ValidationResult("Invalid domain name.");

            return ValidationResult.Success;
        }
    }
}
