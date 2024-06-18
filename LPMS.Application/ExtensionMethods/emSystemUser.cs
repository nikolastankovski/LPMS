namespace LPMS.Application.ExtensionMethods
{
    public static class emSystemUser
    {
        public static ValidationResult Validate(this SystemUser systemUser, CultureInfo ci)
        {
            return new SystemUserValidator(ci).Validate(systemUser);
        }
    }
}
