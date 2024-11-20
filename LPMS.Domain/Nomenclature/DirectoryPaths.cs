namespace LPMS.Domain.Nomenclature
{
    public static class DirectoryPaths
    {
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        public static readonly string BasePath = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName;

        public static readonly string EmailTemplatesPath = Path.Combine(BasePath, "./LPMS.API", "LPMS.EmailService", "EmailTemplates");
    }
}
