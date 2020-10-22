using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettingApp.UnitOfWork;
using BettingApp.Models;

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
        public IActionResult Index()
        {
            // TODO: no 1, first or default, or another way
            var wallet = _unitOfWork.Wallet.FindById(1);

            return Ok(wallet.Balance);
        }

        // TODO: refactor this (first or default...)
        [HttpPost]
        public IActionResult Update([FromBody] Wallet wallet)
        {
            var userWallet = _unitOfWork.Wallet.FindById(1);
            userWallet.Balance = wallet.Balance;
            _unitOfWork.Wallet.Update(userWallet);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
