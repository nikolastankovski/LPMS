using LPMS.Domain.Models.RnRModels.ReferenceModels;

namespace LPMS.Application.ExtensionMethods
{
    public static class emReference
    {
        public static ReferenceWReferenceTypeResponse ToRefWRefTypeResponse(this List<Reference> references, CultureInfo ci)
        {
            if (!references.Any())
                return new ReferenceWReferenceTypeResponse();
            
            string nameAttribute = $"Name_{ci.TwoLetterISOLanguageName.ToUpper()}";
            string descriptionAttribute = $"Description_{ci.TwoLetterISOLanguageName.ToUpper()}";

            var model = new ReferenceWReferenceTypeResponse();

            var referenceType = references.Select(x => x.ReferenceType).First();

            model.ReferenceType = new ReferenceResponse()
            {
                Id = referenceType.ReferenceTypeID,
                Name = referenceType.GetAttribute(nameAttribute),
                Description = referenceType.GetAttribute(descriptionAttribute),
                Code = referenceType.Code
            };

            model.References = references.Select(x => new ReferenceResponse()
            {
                Id = x.ReferenceID,
                Name = x.GetAttribute(nameAttribute),
                Description = x.GetAttribute(descriptionAttribute),
                Code = x.Code
            }).ToList();

            return model;
        }
    }
}
