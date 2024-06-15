﻿namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IBaseRepository<TModel> : ICreateRepository<TModel>, IReadRepository<TModel>, IUpdateRepository<TModel>, IDeleteRepository<TModel> where TModel : class
    {
    }
}
