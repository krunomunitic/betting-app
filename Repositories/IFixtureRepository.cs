using System.Collections.Generic;
using BettingApp.Models;
using System.Linq;

namespace BettingApp.Repositories
{
    public interface IFixtureRepository : IGenericRepository<Fixture>
    {
        IEnumerable<Fixture> GetAllFixtures();
        IEnumerable<Fixture> GetAllFixturesWithSpecialOdds();
    }
}
