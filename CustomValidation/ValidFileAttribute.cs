using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;

namespace CustomValidation
{
    public class ValidFileAttribute : ValidationAttribute
    {
        private bool IsRequired { get; set; }

        private string AllowedFileExtensions { get; set; }

        private const int ImageMinimumBytes = 512;

        public ValidFileAttribute(bool IsRequired = false, string AllowedFileExtensions = "")
        {
            this.IsRequired = IsRequired;
            this.AllowedFileExtensions = AllowedFileExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsRequired == false && value == null)
                return ValidationResult.Success;

            if (value == null)
                return new ValidationResult(string.Format("{0} is required.", validationContext.DisplayName));

            var file = (HttpPostedFileBase)value;
            return IsValid(file)
                ? ValidationResult.Success
                : new ValidationResult(string.Format("{0} is not a valid file.", validationContext.DisplayName));
        }

        private bool IsValid(HttpPostedFileBase postedFile)
        {
            //--------------------------------------------
            //  Check file extension with allowed file extensions
            //--------------------------------------------
            if (!string.IsNullOrEmpty(AllowedFileExtensions))
            {
                if (!AllowedFileExtensions.Split(',').Contains(System.IO.Path.GetExtension(postedFile.FileName)))
                {
                    return false;
                }
            }

            //-------------------------------------------
            //  Check the file's mime type
            //-------------------------------------------
            var contentType = postedFile.ContentType.ToLower();
            if (contentType == "application/vnd.microsoft.portable-executable")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the file extension
            //-------------------------------------------
            var imageExtension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
            if (imageExtension == ".exe")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.InputStream.Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }           

            return true;
        }
    }
}
