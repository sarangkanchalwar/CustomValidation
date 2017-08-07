using System;
using System.ComponentModel.DataAnnotations;

namespace CustomValidation
{
    /// <summary>
    /// Validate string for Active Directory {DomainName}\{Username} format.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class ADUserNameAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        public ADUserNameAttribute(bool isRequired = false)
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

            var inputString = value.ToString();
            if(string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString))
                return new ValidationResult(string.Format(@"{0} can not be left blank.", validationContext.DisplayName));

            string domainName;
            string userName;
            try
            {
                var partsOfInputString = inputString.Split(@"\".ToCharArray());
                domainName = partsOfInputString[0].ToLower().Trim();
                userName = partsOfInputString[1].Trim();
                if (domainName.Length > 0 && userName.Length > 0)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(string.Format(@"{0} is not in valid DomainName\UserName format.", validationContext.DisplayName));
            }
            catch (Exception)
            {
                return new ValidationResult(string.Format(@"{0} is not in valid DomainName\UserName format.", validationContext.DisplayName));
            }
        }
    }
}
