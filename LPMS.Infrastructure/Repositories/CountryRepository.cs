using LPMS.Infrastructure.Repositories.BaseRepositories;

namespace LPMS.Infrastructure.Repositories;

public class CountryRepository(LPMSDbContext context) : BaseRepository<Country, int>(context), ICountryRepository;