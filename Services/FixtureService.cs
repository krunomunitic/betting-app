using System.Linq;
using System.Collections.Generic;
using BettingApp.UnitOfWork;
using BettingApp.Dtos;
using BettingApp.Models;

namespace BettingApp.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FixtureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FixturesDto[] GetFixtures()
        {
            return new FixturesDto[]
            {
                new FixturesDto
                {
                    Name = "Special Offers",
                    Fixtures = GetFixturesWithSpecialOdds(
                        _unitOfWork.Fixtures.GetAllFixturesWithSpecialOdds())
                },
                new FixturesDto
                {
                    Name = "Regular Offers",
                    Fixtures = GetFixtures(
                        _unitOfWork.Fixtures.GetAllFixtures())
                }
            };
        }

        private IList<FixtureDto> GetFixtures(
            IEnumerable<Fixture> fixtures)
        {
            return fixtures.Select(f => new FixtureDto
            {
                Id = f.Id,
                SportName = f.Competition.Sport.Name,
                CompetitionName = f.Competition.Name,
                Date = f.Date,
                HomeTeamName = f.HomeTeam.Name,
                AwayTeamName = f.AwayTeam.Name,
                Result = f.Result,
                Odds = f.FixtureOdds.ToDictionary(
                    fo => fo.Odds.Name,
                    fo => new OddsDto
                    {
                        Id = fo.OddsId,
                        Value = fo.Odds.Value
                    }
                ),
            }).ToList();
        }

        private IList<FixtureDto> GetFixturesWithSpecialOdds(
            IEnumerable<Fixture> fixtures)
        {
            return fixtures.Select(f => new FixtureDto
            {
                Id = f.Id,
                SportName = f.Competition.Sport.Name,
                CompetitionName = f.Competition.Name,
                Date = f.Date,
                HomeTeamName = f.HomeTeam.Name,
                AwayTeamName = f.AwayTeam.Name,
                Result = f.Result,
                Odds = f.FixtureOddsSpecial.ToDictionary(
                    fo => fo.Odds.Name,
                    fo => new OddsDto
                    {
                        Id = fo.OddsId,
                        Value = fo.Odds.Value
                    }
                ),
            }).ToList();
        }
    }
}
