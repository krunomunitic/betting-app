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
            // TODO: add new method to repo Fixtures with defined DTO
            // remove from here, controller can't know about data model
            var fixtures = _unitOfWork.Fixtures.GetComplex(includeProperties: "HomeTeam,AwayTeam,Competition,Offer");
            return Ok(fixtures);
        }
    }
}
