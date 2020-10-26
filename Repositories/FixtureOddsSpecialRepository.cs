using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class FixtureOddsSpecialRepository : GenericRepository<FixtureOddsSpecial>,
        IFixtureOddsSpecialRepository
    {
        public FixtureOddsSpecialRepository(BettingAppContext context) : base(context)
        {
        }
    }
}
