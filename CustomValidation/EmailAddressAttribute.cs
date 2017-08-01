using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace CustomValidation
{
    public class EmailAddressAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
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
                return new ValidationResult(string.Format("{0} can not be left blank. Please provide valid email address.", validationContext.DisplayName));
            else if (value == null && IsRequired == false)
                return ValidationResult.Success;
            try
            {
                var mail = new MailAddress(value.ToString());
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(string.Format("{0} is not a valid email address.", validationContext.DisplayName));
            }
        }
    }
}
