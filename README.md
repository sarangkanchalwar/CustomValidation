# CustomValidation
Custom Validation Attributes contains 3 custom validation attributes for validating Domain Name, HTTP Link & Email Address. 

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

## Licence

Source code can be found on [github](https://github.com/sarangkanchalwar/CustomValidation), licenced under [MIT](http://opensource.org/licenses/mit-license.php).

Developed by [Sarang Kanchalwar](https://stackoverflow.com/users/5756211/sarangk?tab=profile)
