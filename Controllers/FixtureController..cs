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
            var fixtures = _unitOfWork.Fixtures.GetAllFixtures();

            // TODO: create a service for this or
            // do this in repository with Dto model
            var formatted = fixtures.GroupBy(f => f.Competition)
                .ToDictionary(c => c.Key.Name, c => c.Select(c => new
                {
                    id = c.Id,
                    date = c.Date,
                    homeTeamId = c.HomeTeamId,
                    homeTeamName = c.HomeTeam.Name,
                    awayTeamId = c.AwayTeamId,
                    awayTeamName = c.AwayTeam.Name,
                    offer = new
                    {
                        home = c.Offer.Home,
                        draw = c.Offer.Draw,
                        away = c.Offer.Away
                    },
                    result = c.Result,
                }));

            return Ok(formatted);
        }
    }
}
