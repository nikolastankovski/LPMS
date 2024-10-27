namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IDeleteRepository<TModel, PkType> 
        where TModel : class
        where PkType : struct
    {
        void Delete(PkType id);
        //Result<TModel> Delete(List<object> ids, string culture);
        Task DeleteAsync(PkType id);
        //Task<Result<TModel>> DeleteAsync(List<object> ids, string culture);
    }
}
