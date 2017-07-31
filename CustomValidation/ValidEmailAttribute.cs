using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace CustomValidation
{
    public class ValidEmailAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// Returns true if email is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Email address required.");
            try
            {
                var mail = new MailAddress(value.ToString());
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid email address.");
            }
        }
    }
}
