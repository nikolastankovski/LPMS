using Serilog;
using System.Globalization;

namespace LPMS.Application.ExtensionMethods
{
    public static class emCommon
    {
        public static Guid ToGuid(this string str)
        {
            var tryParse = Guid.TryParse(str, out var result);

            if(tryParse)
                return result;
            else
                return Guid.Empty;
        }

        public static string ToFullDateTime(this DateTime? dateTime, CultureInfo ci)
        {
            string dateTimeFormat = Resources.ResourceManager.GetString(nameof(Resources.DT_FullDateFormat), ci) ?? string.Empty;
            
            try
            {
                if (dateTime.HasValue && !string.IsNullOrEmpty(dateTimeFormat)) 
                    return dateTime.Value.ToString(dateTimeFormat);

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }

            return string.Empty;
        }

        public static string ToFullDateTime(this DateTime dateTime, CultureInfo ci)
        {
            string dateTimeFormat = Resources.ResourceManager.GetString(nameof(Resources.DT_FullDateFormat), ci) ?? string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(dateTimeFormat))
                    return dateTime.ToString(dateTimeFormat);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }

            return string.Empty;
        }
    }
}
