using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BettingApp.Services;
using BettingApp.Dtos;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly ICompetitionService _competitionService;
        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<CompetitionSportsDto> competitionsBySports = _competitionService.GetCompetitionDetails();

            return Ok(competitionsBySports);
        }
    }
}
