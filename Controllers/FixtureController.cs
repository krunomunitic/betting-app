using Microsoft.AspNetCore.Mvc;
using BettingApp.Services;
using BettingApp.Dtos;

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
            FixturesDto fixtures = _fixtureService.GetFixtures();
            return Ok(fixtures);
        }
    }
}
