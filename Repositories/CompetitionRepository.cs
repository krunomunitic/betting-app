using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(BettingAppContext context) : base(context)
        {
        }
    }
}