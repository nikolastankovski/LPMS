using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Application.ExtensionMethods
{
    public static class emCultureInfo
    {
        public static string GetResource(this CultureInfo culture, string resourceName)
        {
            var resource = Resources.ResourceManager.GetString(resourceName, culture);

            return resource ?? string.Empty;
        }
    }
}
