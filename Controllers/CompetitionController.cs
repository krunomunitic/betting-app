using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.Services;

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
            var competitionsBySports = _competitionService.GetCompetitionDetails();

            return Ok(competitionsBySports);
        }
    }
}
