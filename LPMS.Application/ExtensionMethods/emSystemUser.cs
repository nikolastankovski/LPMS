namespace LPMS.Application.ExtensionMethods
{
    public static class emSystemUser
    {
        public static ValidationResult Validate(this SystemUser systemUser, CultureInfo ci, ISystemUserRepository systemUserRepository)
        {
            return new SystemUserValidator(ci, systemUserRepository).Validate(systemUser);
        }
    }
}
