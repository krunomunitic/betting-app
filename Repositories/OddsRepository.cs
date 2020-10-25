using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class OddsRepository : GenericRepository<Odds>, IOddsRepository
    {
        public OddsRepository(BettingAppContext context) : base(context)
        {
        }
    }
}
