# CustomValidation
The CustomValidation project has various custom attributes for validating data. It is based on Data Annotations. These attributes validate data & also secures application accepting malicious containts. Following are the custom attributes-
* [File Validation Attribute](#file-validation-attribute)
* [Image File Validation Attribute](#Image-file-validation-attribute)
* [DateTime Validation Attribute](#DateTime-validation-attribute)
* Http Link Validation Attribute
* Domain Name Validation Attribute
* Active Directory Username Validation Attribute

## Install via NuGet
The library is available as [NuGet package](https://www.nuget.org/packages/CustomValidation) too.
To install Custom Validation, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)

```bash
PM> Install-Package CustomValidation
```
## Method of Use
* It's like one another Data Anotation attribute.
* All attributes are `optional` by default.
* To made `[Required]` needs to set `IsRequired` value `true` Ex. `[DomainName(IsRequired = true)]`

## File Validation Attribute
This attribute is used to validate files to be uploaded on server. This can be used while uploading file as [HttpPostedFileBase](https://msdn.microsoft.com/en-us/library/system.web.httppostedfilebase(v=vs.110).aspx) class. It validates file with extension, mime-types & containts. It also validate for malicious containts. It can be used as any normal Data Annotation attribute.

```bash
[ValidFile]
public HttpPostedFileBase Portfolio { get; set; }
```

By default it's optional, but it has several other options like-
* `[ValidFile(IsRequired = true)]` - This will make it manditory field.
* `[ValidFile(AllowedFileExtensions = ".doc,.docx,.pdf")]` - This will only accept `.doc`, `.docx`, `.pdf` file extensions.
* `[ValidFile(ErrorMessage = "Invalid portfolio file.")]` - This will print error message as *Invalid portfolio file.* if found uploaded file invalid.
* `[ValidFile(IsRequired = true, AllowedFileExtensions = ".doc,.docx,.pdf", ErrorMessage = "Invalid portfolio file.")]` - All combinations are allowed.

## Image File Validation Attribute
This attribute is used to validate images to be uploaded on server. This can be used while uploading image files as [HttpPostedFileBase](https://msdn.microsoft.com/en-us/library/system.web.httppostedfilebase(v=vs.110).aspx) class. It validates file with extension, mime-types & ensure its an image. It also validate for malicious containts. It can be used as any normal Data Annotation attribute.

```bash
[ImageFile]
public HttpPostedFileBase ProfileImage { get; set; }
```

By default it's optional, but it has several other options like-
* `[ImageFile(IsRequired = true)]` - This will make it manditory field.
* `[ImageFile(AllowedFileExtensions = ".jpg,.jpeg,.png")]` - This will only accept `.jpg`, `.jpeg`, `.png` images only.
* `[ImageFile(ErrorMessage = "Invalid profile image.")]` - This will print error message as *Invalid profile image.* if found uploaded file invalid.
* `[ImageFile(IsRequired = true, AllowedFileExtensions = ".jpg,.jpeg,.png", ErrorMessage = "Invalid profile image.")]` - All combinations are allowed.

## DateTime Validation Attribute
`ValidDateTime` attribute 

## Licence

Source code can be found on [github](https://github.com/sarangkanchalwar/CustomValidation), licenced under [MIT](http://opensource.org/licenses/mit-license.php).

Developed by [Sarang Kanchalwar](https://in.linkedin.com/in/sarang-kanchalwar-93396b83)
