using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomValidation
{
    public class ValidHttpLinkAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Can not be left blank. Please provide valid link(s).");
            var urlStrings = value.ToString();
            var lstUrls = urlStrings.Split(',').ToList();
            foreach (var url in lstUrls)
            {
                Uri uriResult;
                var result = Uri.TryCreate(url.Trim(), UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!result) return new ValidationResult("One(or more) invalid URL(s).");
            }
            return ValidationResult.Success;
        }
    }
}
