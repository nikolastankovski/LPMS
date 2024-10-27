using LPMS.Domain.Nomenclature;

namespace LPMS.EmailService.EmailTemplates
{
    public static class EmailTemplates
    {
        private static readonly string TemplatePath = DirectoryPaths.EmailTemplatesPath;
        public const string Layout = "_EmailTemplateLayout.cshtml";

        public static readonly string Account_ForgotPassword = Path.Combine(TemplatePath, "Account_ForgotPassword.cshtml");
        public static readonly string Account_ConfirmEmail = Path.Combine(TemplatePath, "Account_ConfirmEmail.cshtml");
    }
}