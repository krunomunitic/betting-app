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

            // TODO: create a service for this or
            // do this in repository with Dto model
            var formatted = allFixtures.GroupBy(f => f.Competition)
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
                        special = f.Special,
                        odds = f.FixtureOdds.ToDictionary(fo => fo.Odds.Name,
                        fo => new {
                            id = fo.OddsId,
                            value = fo.Odds.Value
                        }),
                        result = f.Result,
                    }).ToList()
                }).ToList();

            return Ok(formatted);
        }
    }
}
