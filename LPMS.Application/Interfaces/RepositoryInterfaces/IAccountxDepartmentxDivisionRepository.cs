namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface IAccountxDepartmentxDivisionRepository : ICreateRepository<AccountxDepartmentxDivision>, IReadRepository<AccountxDepartmentxDivision>
    {
        Task<List<AccountxDepartmentxDivision>> GetByAccountIdAsync(Guid accountId);
        Task<List<AccountxDepartmentxDivision>> GetBySystemUserIdAsync(Guid systemUserId);
    }
}
