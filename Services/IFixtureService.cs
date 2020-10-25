using System.Collections.Generic;
using BettingApp.Dtos;

namespace BettingApp.Services
{
    public interface IFixtureService
    {
        public FixturesDto GetFixtures();
    };
}
