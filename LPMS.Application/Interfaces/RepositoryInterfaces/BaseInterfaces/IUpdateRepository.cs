﻿using System.Globalization;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        void Update(TModel entity, Guid modifiedBy, CultureInfo culture);
        //Result<TModel> Update(List<TModel> entities, Guid modifiedBy, string culture);
        Task UpdateAsync(TModel entity, Guid modifiedBy, CultureInfo culture);
        //Task<Result<TModel>> UpdateAsync(List<TModel> entities, Guid modifiedBy, string culture);
    }
}