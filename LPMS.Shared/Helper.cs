using PasswordGenerator;

namespace LPMS.Shared
{
    public static class Helper
    {
        public static string GeneratePassword(int passwordLength = 8)
        {
            return new Password(passwordLength).IncludeLowercase().IncludeUppercase().IncludeNumeric().IncludeSpecial().Next();
        }
    }
}
