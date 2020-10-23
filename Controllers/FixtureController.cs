using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.Repositories;
using BettingApp.UnitOfWork;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixtureController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FixtureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var allFixtures = _unitOfWork.Fixtures.GetAllFixtures();
            var fixturesWithSpecialOdds = _unitOfWork.Fixtures.GetAllFixturesWithSpecialOdds();

            // TODO: create a service for this or
            // do this in repository with Dto model
            // in service refactor duplicate code
            var fixturesByCompetition = allFixtures.GroupBy(f => f.Competition)
                .Select(c => new
                {
                    competitionName = c.Key.Name,
                    fixtures = c.Select(f => new
                    {
                        id = f.Id,
                        date = f.Date,
                        homeTeamId = f.HomeTeamId,
                        homeTeamName = f.HomeTeam.Name,
                        awayTeamId = f.AwayTeamId,
                        awayTeamName = f.AwayTeam.Name,
                        odds = f.FixtureOdds.ToDictionary(fo => fo.Odds.Name,
                        fo => new {
                            id = fo.OddsId,
                            value = fo.Odds.Value
                        }),
                        result = f.Result,
                    }).ToList()
                }).ToList();

            var fixturesByCompetitionSpecial = fixturesWithSpecialOdds.GroupBy(f => f.Competition)
                .Select(c => new
                {
                    competitionName = c.Key.Name,
                    fixtures = c.Select(f => new
                    {
                        id = f.Id,
                        date = f.Date,
                        homeTeamId = f.HomeTeamId,
                        homeTeamName = f.HomeTeam.Name,
                        awayTeamId = f.AwayTeamId,
                        awayTeamName = f.AwayTeam.Name,
                        odds = f.FixtureOddsSpecial.ToDictionary(fo => fo.Odds.Name,
                        fo => new {
                            id = fo.OddsId,
                            value = fo.Odds.Value
                        }),
                        result = f.Result,
                    }).ToList()
                }).ToList();

            return Ok(new { fixturesByCompetition, fixturesByCompetitionSpecial });
        }
    }
}
