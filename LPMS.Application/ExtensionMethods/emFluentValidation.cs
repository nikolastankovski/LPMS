using FluentValidation.Results;
using LanguageExt.Common;

namespace LPMS.Application.ExtensionMethods
{
    public static class emFluentValidation
    {
        public static List<string> GetErrors(this ValidationResult? valResult)
        {
            if (valResult == null) return new List<string>();

            return valResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
}
