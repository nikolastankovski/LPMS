using System.Globalization;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace LPMS.Application.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator(CultureInfo ci)
        {
            var rm = Resources.ResourceManager;
            string vldMsgIsReq = nameof(Resources.VLDMSG_Is_Required);

            RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithMessage(rm.GetString(vldMsgIsReq, ci).Replace("{AttributeName}", rm.GetString(nameof(Resources.Name_EN), ci)))
                        .MaximumLength(256)
                        .WithMessage(Resources.VLDMSG_Max_Chars.Replace("{AttributeName}", Resources.Name_EN).Replace("{MaxChars}", "256"));

        }   
    }
}
