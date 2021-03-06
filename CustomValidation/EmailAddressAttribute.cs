﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace CustomValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAddressAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public bool IsRequired { get; set; }

        public EmailAddressAttribute(bool isRequired = false)
        {
            IsRequired = isRequired;
        }

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
            else if (value == null && IsRequired == false)
                return ValidationResult.Success;
            try
            {
                var mail = new MailAddress(value.ToString());
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(string.Format("{0} is not a valid.", validationContext.DisplayName));
            }
        }
    }
}
