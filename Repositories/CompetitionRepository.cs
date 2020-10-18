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

        public IEnumerable<Competition> GetCompetitionsBySports()
        {
            return _context.Competitions.Include(c=> c.Sport).ToList();
        }
    }
}