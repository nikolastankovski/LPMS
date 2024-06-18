namespace LPMS.Application.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator(CultureInfo ci)
        {
            string isRequired = ci.GetResource(nameof(Resources.VLDMSG_Is_Required));
            string maxChars = ci.GetResource(nameof(Resources.VLDMSG_Max_Chars));

            RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithName(ci.GetResource(nameof(Resources.Name)))
                        .WithMessage(isRequired)
                        .MaximumLength(256)
                        .WithMessage(maxChars.Replace("{MaxChars}", "256"));

            RuleFor(x => x.SystemUserId)
                .NotEmpty()
                .WithMessage(isRequired);

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithName(ci.GetResource(nameof(Resources.CreatedBy)))
                .WithMessage(isRequired);

        }   
    }
}
