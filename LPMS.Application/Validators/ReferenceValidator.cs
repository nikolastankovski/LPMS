#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace LPMS.Application.Validators
{
    public class ReferenceValidator : AbstractValidator<Reference>
    {
        public ReferenceValidator(CultureInfo ci) 
        {
            string isRequired = ci.GetResource(nameof(Resources.VLDMSG_Is_Required));
            string maxChars = ci.GetResource(nameof(Resources.VLDMSG_Max_Chars));

            RuleFor(x => x.Name_EN)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Name_EN)))
                        .WithMessage(isRequired)
                        .MaximumLength(256)
                        .WithMessage(maxChars.Replace("{MaxChars}", "256"));

            RuleFor(x => x.Name_MK)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Name_MK)))
                        .WithMessage(isRequired)
                        .MaximumLength(256)
                        .WithMessage(maxChars.Replace("{MaxChars}", "256"));

            RuleFor(x => x.Description_EN)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Description_EN)))
                        .WithMessage(isRequired)
                        .MaximumLength(500)
                        .WithMessage(maxChars.Replace("{MaxChars}", "500"));

            RuleFor(x => x.Description_MK)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Description_MK)))
                        .WithMessage(isRequired)
                        .MaximumLength(500)
                        .WithMessage(maxChars.Replace("{MaxChars}", "500"));

            RuleFor(x => x.Code)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Code)))
                        .WithMessage(isRequired)
                        .MaximumLength(50)
                        .WithMessage(maxChars.Replace("{MaxChars}", "50"));
        }
    }
}
