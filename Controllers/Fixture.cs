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
            var fixtures = _unitOfWork.Fixtures.GetAll();
            return Ok(fixtures);
        }
    }
}
