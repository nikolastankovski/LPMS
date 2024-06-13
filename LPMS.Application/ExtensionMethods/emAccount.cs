using FluentValidation.Results;
using LPMS.Application.Validators;
using System.Globalization;

namespace LPMS.Application.ExtensionMethods
{
    public static class emAccount
    {
        public static ValidationResult Validate(this Account account, CultureInfo ci)
        {
            return new AccountValidator(ci).Validate(account);
        }
    }
}
