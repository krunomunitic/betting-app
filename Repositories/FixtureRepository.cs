using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class FixtureRepository : GenericRepository<Fixture>, IFixtureRepository
    {
        public FixtureRepository(BettingAppContext context): base(context)
        {
        }
    }
}
