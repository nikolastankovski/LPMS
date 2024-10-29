namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IModifyRepository<TModel> where TModel : class
    {
        void Modify(TModel entity);
        //Result<TModel> Update(List<TModel> entities, Guid modifiedBy, string culture);
        Task ModifyAsync(TModel entity);
        //Task<Result<TModel>> UpdateAsync(List<TModel> entities, Guid modifiedBy, string culture);
    }
}