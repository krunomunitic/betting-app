using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class FixtureRepository : GenericRepository<Fixture>, IFixtureRepository
    {
        public FixtureRepository(BettingAppContext context): base(context)
        {
        }

        // TODO: get all in one DB get

        public IEnumerable<Fixture> GetAllFixtures()
        {
            return _context.Fixtures
                .Include(f => f.AwayTeam)
                .Include(f => f.HomeTeam)
                .Include(f => f.Competition).ThenInclude(c => c.Sport)
                .Include(f => f.FixtureOdds).ThenInclude(fo => fo.Odds)
                .ToList();
        }

        public IEnumerable<Fixture> GetAllFixturesWithSpecialOdds()
        {
            return _context.Fixtures
                .Where(f => f.FixtureOddsSpecial.Any())
                .Include(f => f.AwayTeam)
                .Include(f => f.HomeTeam)
                .Include(f => f.Competition).ThenInclude(c => c.Sport)
                .Include(f => f.FixtureOddsSpecial).ThenInclude(fo => fo.Odds)
                .ToList();
        }
    }
}
