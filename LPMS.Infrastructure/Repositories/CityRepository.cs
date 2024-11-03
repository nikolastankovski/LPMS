using LPMS.Infrastructure.Repositories.BaseRepositories;

namespace LPMS.Infrastructure.Repositories;

public class CityRepository(LPMSDbContext context) : BaseRepository<City, int>(context), ICityRepository;