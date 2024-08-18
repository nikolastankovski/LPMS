using LPMS.Domain.Nomenclature;

namespace LPMS.EmailService.EmailTemplates
{
    public static class EmailTemplates
    {
        public static readonly string TemplatePath = DirectoryPaths.EmailTemplatesPath;

        public static readonly string Account_ForgotPassword = Path.Combine(TemplatePath, "Account_ForgotPassword.cshtml");
    }
}