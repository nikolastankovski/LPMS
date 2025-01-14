using Endpoint = Microsoft.AspNetCore.Http.Endpoint;

namespace LPMS.Application.Interfaces.RepositoryInterfaces;

public interface IEndpointRepository : IBaseRepository<Endpoint, int>;