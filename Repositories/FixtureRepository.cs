using System;
using System.Collections.Generic;
using System.Linq;
using BettingApp.Data;
using BettingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingApp.Repositories
{
    public class FixtureRepository : GenericRepository<Fixture>, IFixtureRepository
    {
        public FixtureRepository(BettingAppContext context): base(context)
        {
        }

        // TODO: select only whats needed, do grouping here
        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _context.Fixtures
                .Include(f => f.AwayTeam)
                .Include(f => f.HomeTeam)
                .Include(f => f.Competition)
                .Include(f => f.FixtureOdds).ThenInclude(fo => fo.Odds)
                .ToList();
        }

        // TODO: select only whats needed, do grouping here
        public IEnumerable<Fixture> GetAllFixturesWithSpecialOdds()
        {
            return _context.Fixtures.Where(f => f.FixtureOddsSpecial.Any())
                .Include(f => f.AwayTeam)
                .Include(f => f.HomeTeam)
                .Include(f => f.Competition)
                .Include(f => f.FixtureOddsSpecial).ThenInclude(fo => fo.Odds)
                .ToList();
        }
    }
}
