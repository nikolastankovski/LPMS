namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IBaseRepository<TModel, TPkType> : ICreateRepository<TModel>, IReadRepository<TModel, TPkType>, IModifyRepository<TModel>, IDeleteRepository<TModel, TPkType> 
        where TModel : class
        where TPkType : struct
    {
    }
}
