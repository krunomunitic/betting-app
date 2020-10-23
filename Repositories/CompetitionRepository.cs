using System;
using System.Collections.Generic;
using System.Linq;
using BettingApp.Data;
using BettingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingApp.Repositories
{
    public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(BettingAppContext context) : base(context)
        {
        }

        // TODO: select only whats needed, do grouping here
        public IEnumerable<Competition> GetCompetitionsBySports()
        {
            return _context.Competitions.Include(c=> c.Sport).ToList();
        }
    }
}