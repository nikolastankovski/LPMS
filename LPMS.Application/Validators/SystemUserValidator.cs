namespace LPMS.Application.Validators
{
    public class SystemUserValidator : AbstractValidator<SystemUser>
    {
        public SystemUserValidator(CultureInfo ci, ISystemUserRepository systemUserRepository)
        {
            string isRequired = ci.GetResource(nameof(Resources.VLDMSG_Is_Required));
            string maxChars = ci.GetResource(nameof(Resources.VLDMSG_Max_Chars));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithName(ci.GetResource(nameof(Resources.Email)))
                .WithMessage(isRequired)
                .MustAsync(async (email, c) => await systemUserRepository.IsEmailUsedAsync(email))
                .EmailAddress()
                .WithMessage(ci.GetResource(nameof(Resources.Email_InvalidFormat)))
                .MaximumLength(256)
                .WithMessage(maxChars.Replace("{MaxChars}", "256"));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithName(ci.GetResource(nameof(Resources.Username)))
                .WithMessage(isRequired)
                .MaximumLength(256)
                .WithMessage(maxChars.Replace("{MaxChars}", "256"));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithName(ci.GetResource(nameof(Resources.PhoneNumber)))
                .WithMessage(isRequired);
        }
    }
}
