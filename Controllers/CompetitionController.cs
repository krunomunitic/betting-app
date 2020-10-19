using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.UnitOfWork;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompetitionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var competitions = _unitOfWork.Competition.GetCompetitionsBySports();

            // TODO: create a service for this or
            // do this in repository with Dto model
            var formatted = competitions.GroupBy(c => c.Sport)
                .ToDictionary(s => s.Key.Name, s => s.Select(c => new {
                    id = c.Id,
                    name = c.Name
                }).ToList());

            return Ok(formatted);
        }
    }
}
