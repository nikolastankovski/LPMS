namespace LPMS.Application.ExtensionMethods
{
    public static class emException
    {
        public static string ToMessageTemplate(this Exception exception)
        {
            return $"Exception: {exception.Message}; Inner Exception: {exception?.InnerException?.Message}";
        }
    }
}
