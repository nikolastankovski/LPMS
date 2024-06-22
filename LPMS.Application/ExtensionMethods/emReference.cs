using LPMS.Domain.Models.DTO;
using System.Linq;

namespace LPMS.Application.ExtensionMethods
{
    public static class emReference
    {
        public static List<DTOReference> ToDTO(this List<Reference> references, CultureInfo ci)
        {
            string nameAttribute = $"Name_{ci.TwoLetterISOLanguageName.ToUpper()}";
            string descriptionAttribute = $"Description_{ci.TwoLetterISOLanguageName.ToUpper()}";

            return references.Select(x => 
                new DTOReference(
                    x.ReferenceID,
                    x.GetAttribute(nameAttribute),
                    x.GetAttribute(descriptionAttribute),
                    x.Code
                )
            ).ToList();
        }
    }
}
