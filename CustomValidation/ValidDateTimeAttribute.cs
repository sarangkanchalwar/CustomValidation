using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CustomValidation
{
    /// <summary>
    /// Validate a string to be a parse-able DateTime string with specified format.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidDateTimeAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        public string Format { get; set; }
        
        public ValidDateTimeAttribute(bool isRequired = false, string format = "dd/MM/yyyy")
        {
            IsRequired = isRequired;
            Format = format;
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

            var dateString = value.ToString();
            DateTime dateTime;
            if (DateTime.TryParseExact(dateString, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return ValidationResult.Success;
            else
                return new ValidationResult(string.Format("{0} is not in {1} format.", validationContext.DisplayName, Format));
        }
    }
}
