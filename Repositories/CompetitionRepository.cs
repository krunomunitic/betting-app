using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(BettingAppContext context) : base(context)
        {
        }

        public IEnumerable<Competition> GetCompetitionsBySports()
        {
            return _context.Competitions.Include(c=> c.Sport).ToList();
        }
    }
}