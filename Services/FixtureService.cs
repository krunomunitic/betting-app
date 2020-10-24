using BettingApp.UnitOfWork;
using System.Collections.Generic;
using BettingApp.Dtos;
using BettingApp.Models;
using System.Linq;

namespace BettingApp.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FixtureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // TODO: add automapper
        public FixturesDto GetFixtures()
        {
            return new FixturesDto
            {
                FixturesByCompetition = GetFixturesByCompetition(
                    _unitOfWork.Fixtures.GetAllFixtures()),
                FixturesByCompetitionSpecial = GetFixturesByCompetition(
                    _unitOfWork.Fixtures.GetAllFixturesWithSpecialOdds())
            };
        }

        private IList<FixturesByCompetitionDto> GetFixturesByCompetition(IEnumerable<Fixture> fixtures)
        {
            return fixtures.GroupBy(f => f.Competition)
                .Select(c => new FixturesByCompetitionDto
                {
                    CompetitionName = c.Key.Name,
                    Fixtures = c.Select(f => new FixtureDto
                    {
                        Id = f.Id,
                        Date = f.Date,
                        HomeTeamId = f.HomeTeamId,
                        HomeTeamName = f.HomeTeam.Name,
                        AwayTeamId = f.AwayTeamId,
                        AwayTeamName = f.AwayTeam.Name,
                        Result = f.Result,
                        Odds = f.FixtureOdds.ToDictionary(
                            fo => fo.Odds.Name,
                            fo => new OddsDto
                            {
                                Id = fo.OddsId,
                                Value = fo.Odds.Value
                            }),
                    }).ToList()
                }).ToList();
        }
    }
}
