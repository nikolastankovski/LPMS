using Serilog;
using System.Globalization;
using System.Runtime.InteropServices;

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
            string dateTimeFormat = ci.GetResource(nameof(Resources.DT_FullDateFormat));
            
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
            string dateTimeFormat = ci.GetResource(nameof(Resources.DT_FullDateFormat));

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

        public static string GetAttribute(this object obj, string name)
        {
            return Convert.ToString(obj?.GetType()?.GetProperty(name)?.GetValue(obj)) ?? string.Empty;
        }
    }
}
