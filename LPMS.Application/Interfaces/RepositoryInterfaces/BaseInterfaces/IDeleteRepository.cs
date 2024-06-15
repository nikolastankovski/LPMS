namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IDeleteRepository<TModel> where TModel : class
    {
        bool Delete(object id);
        //Result<TModel> Delete(List<object> ids, string culture);
        Task<bool> DeleteAsync(object id);
        //Task<Result<TModel>> DeleteAsync(List<object> ids, string culture);
    }
}
