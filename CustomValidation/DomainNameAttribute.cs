﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DomainNameAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        public DomainNameAttribute(bool isRequired = false)
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

            var domain = value.ToString();
            if (!domain.Contains('.'))
                return new ValidationResult(string.Format("{0} is not a valid.", validationContext.DisplayName));

            var charCount = Regex.Matches(domain, @"[a-z]").Count;
            var periodCount = Regex.Matches(domain, @"[.]").Count;
            if (domain.Length != (charCount + periodCount))
                return new ValidationResult(string.Format("{0} is not a valid.", validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
