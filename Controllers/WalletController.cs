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
    public class WalletController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WalletController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            // TODO: no 0, first or default, or another way
            var wallet = _unitOfWork.Wallet.FindById(1);

            return Ok(wallet);
        }

        // TODO: refactor this (first or default...)
        [HttpPost]
        public ActionResult Submit(int balance)
        {
            var wallet = _unitOfWork.Wallet.FindById(1);
            wallet.Balance = balance;
            _unitOfWork.Wallet.Update(wallet);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
