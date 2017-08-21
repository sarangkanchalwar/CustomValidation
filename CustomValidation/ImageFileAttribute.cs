using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;

namespace CustomValidation
{
    public class ImageFileAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }

        private const int ImageMinimumBytes = 512;

        public ImageFileAttribute(bool isRequired = false)
        {
            IsRequired = isRequired;
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
                : new ValidationResult(string.Format("{0} is not a valid image file.", validationContext.DisplayName));
        }

        private bool IsValid(HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            var contentType = postedFile.ContentType.ToLower();
            if (contentType != "image/jpg" &&
              contentType != "image/jpeg" &&
              contentType != "image/pjpeg" &&
              contentType != "image/x-png" &&
              contentType != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var imageExtension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
            if (imageExtension != ".jpg"
                && imageExtension != ".png"
                && imageExtension != ".jpeg"
                && imageExtension != ".bmp")
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

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
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