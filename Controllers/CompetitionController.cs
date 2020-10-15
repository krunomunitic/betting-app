using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.UnitOfWork;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            // TODO: add new method to repo Competition with defined DTO
            // remove from here, controller can't know about data model
            var competitions = _unitOfWork.Competition.GetComplex(includeProperties: "Sport");
            return Ok(competitions);
        }
    }
}
