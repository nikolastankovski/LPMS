namespace LPMS.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country, int>, ICountryRepository
    {
        public CountryRepository(LPMSDbContext context) : base(context)
        {
        }
    }
}
