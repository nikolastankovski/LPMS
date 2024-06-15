using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Application.ExtensionMethods
{
    public static class emFluentValidation
    {
        public static List<string> GetErrors(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return new List<string>();

            return validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
