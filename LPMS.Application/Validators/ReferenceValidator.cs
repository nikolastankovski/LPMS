using System.Globalization;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace LPMS.Application.Validators
{
    public class ReferenceValidator : AbstractValidator<Reference>
    {
        public ReferenceValidator(CultureInfo ci) 
        {
            var rm = Resources.ResourceManager;
            string vldMsgIsReq = nameof(Resources.VLDMSG_Is_Required);
            //string vldMsgMaxChars = nameof(Resources.VLDMSG_Max_Chars);

            RuleFor(x => x.Name_EN)
                        .NotEmpty()
                        .WithMessage(rm.GetString(vldMsgIsReq, ci).Replace("{AttributeName}", rm.GetString(nameof(Resources.Name_EN), ci)))
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
    }
}
