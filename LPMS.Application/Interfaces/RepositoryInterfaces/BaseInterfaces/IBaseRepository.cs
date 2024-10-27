namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IBaseRepository<TModel, PkType> : ICreateRepository<TModel>, IReadRepository<TModel, PkType>, IUpdateRepository<TModel>, IDeleteRepository<TModel, PkType> 
        where TModel : class
        where PkType : struct
    {
    }
}
