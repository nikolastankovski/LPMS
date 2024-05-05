using System.Globalization;
using System.Text.RegularExpressions;

namespace LPMS.Application.Validators
{
    public class ReferenceValidator : AbstractValidator<Reference>
    {
        private CultureInfo _clt = CultureInfo.GetCultureInfo("en-GB");
        public ReferenceValidator() 
        {
            var rm = Resources.ResourceManager;
            string vldMsgIsReq = nameof(Resources.VLDMSG_Is_Required);
            string vldMsgMaxChars = nameof(Resources.VLDMSG_Max_Chars);

            RuleFor(x => x.Name_EN)
                        .NotEmpty()
                        .WithMessage(rm.GetString(vldMsgIsReq, _clt).Replace("{AttributeName}", rm.GetString(nameof(Resources.Name_EN), _clt)))
                        .MaximumLength(256)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Name_EN).Replace("{MaxChars}", "256"));

            RuleFor(x => x.Name_MK)
                        .NotEmpty()
                        .WithMessage(Resources.VLDMSG_Is_Required.Replace("{AttributeName}", Resources.Name_MK))
                        .MaximumLength(256)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Name_MK).Replace("{MaxChars}", "256"));


            RuleFor(x => x.Description_EN)
                        .NotEmpty()
                        .WithMessage(Resources.VLDMSG_Is_Required.Replace("{AttributeName}", Resources.Description_EN))
                        .MaximumLength(500)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Description_EN).Replace("{MaxChars}", "500"));

            RuleFor(x => x.Description_MK)
                        .NotEmpty()
                        .WithMessage(Resources.VLDMSG_Is_Required.Replace("{AttributeName}", Resources.Description_MK))
                        .MaximumLength(500)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Description_MK).Replace("{MaxChars}", "500"));

            RuleFor(x => x.Code)
                        .NotEmpty()
                        .WithMessage(Resources.VLDMSG_Is_Required.Replace("{AttributeName}", Resources.Code))
                        .MaximumLength(50)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Code).Replace("{MaxChars}", "50"));
        }

        public void SetCulture(string culture = "en-GB")
        {
            _clt = CultureInfo.GetCultureInfo(culture);
        }
    }
}
