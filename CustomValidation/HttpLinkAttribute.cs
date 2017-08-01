using System;
using System.ComponentModel.DataAnnotations;

namespace CustomValidation
{
    public class HttpLinkAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && IsRequired)
                return new ValidationResult(string.Format("{0} can not be left blank. Please provide valid URL.", validationContext.DisplayName));
            else if(value == null && IsRequired == false)
                return ValidationResult.Success;
            var urlString = value.ToString();
            Uri uriResult;
            var result = Uri.TryCreate(urlString.Trim(), UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result) return new ValidationResult(string.Format("{0} is not a valid URL.", validationContext.DisplayName));
            return ValidationResult.Success;
        }
    }
}
