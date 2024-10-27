namespace LPMS.Application.ExtensionMethods
{
    public static class emSystemUser
    {
        public async static Task<ValidationResult> ValidateAsync(this SystemUser systemUser, CultureInfo ci, ISystemUserRepository systemUserRepository)
        {
            return await new SystemUserValidator(ci, systemUserRepository).ValidateAsync(systemUser);
        }
    }
}
