namespace LPMS.Application.Validators;

public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator(CultureInfo ci)
    {
        string isRequired = ci.GetResource(nameof(Resources.VLDMSG_Is_Required));
        string maxChars = ci.GetResource(nameof(Resources.VLDMSG_Max_Chars));

        RuleFor(x => x.Name_EN)
            .NotEmpty()
            .WithName(ci.GetResource(nameof(Resources.Name_EN)))
            .WithMessage(isRequired)
            .MaximumLength(256)
            .WithMessage(maxChars.Replace("{MaxChars}", "500"));
        
        RuleFor(x => x.Name_MK)
            .NotEmpty()
            .WithName(ci.GetResource(nameof(Resources.Name_MK)))
            .WithMessage(isRequired)
            .MaximumLength(256)
            .WithMessage(maxChars.Replace("{MaxChars}", "500"));
    }
}