using System;
using System.ComponentModel.DataAnnotations;

namespace CustomValidation
{
    public class HttpLinkAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && IsRequired)
                return new ValidationResult(string.Format("{0} can not be left blank.", validationContext.DisplayName));
            else if(value == null && IsRequired == false)
                return ValidationResult.Success;
            var urlString = value.ToString();
            Uri uriResult;
            var result = Uri.TryCreate(urlString.Trim(), UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result) return new ValidationResult(string.Format("{0} is not a valid.", validationContext.DisplayName));
            return ValidationResult.Success;
        }
    }
}
