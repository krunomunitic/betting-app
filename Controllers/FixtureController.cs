using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.Repositories;
using BettingApp.Services;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixtureController : ControllerBase
    {
        private readonly IFixtureService _fixtureService;
        public FixtureController(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var fixtures = _fixtureService.GetFixtures();
            return Ok(fixtures);
        }
    }
}
