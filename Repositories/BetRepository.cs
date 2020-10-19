using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class BetRepository: GenericRepository<Bet>, IBetRepository
    {
        public BetRepository(BettingAppContext context): base(context)
        {
        }
    }
}
