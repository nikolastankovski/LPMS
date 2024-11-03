namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IDeleteRepository<TModel, TPkType> 
        where TModel : class
        where TPkType : struct
    {
        void Delete(TPkType id);
        void Delete(TModel entity);
        //Result<TModel> Delete(List<object> ids, string culture);
        Task DeleteAsync(TPkType id);
        Task DeleteAsync(TModel entity);

        //Task<Result<TModel>> DeleteAsync(List<object> ids, string culture);
    }
}
