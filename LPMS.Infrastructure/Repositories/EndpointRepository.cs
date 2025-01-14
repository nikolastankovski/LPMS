using LPMS.Infrastructure.Repositories.BaseRepositories;
using Endpoint = Microsoft.AspNetCore.Http.Endpoint;

namespace LPMS.Infrastructure.Repositories;

public class EndpointRepository : BaseRepository<Endpoint, int>, IEndpointRepository
{
    public EndpointRepository(LPMSDbContext context) : base(context)
    {
    }
}