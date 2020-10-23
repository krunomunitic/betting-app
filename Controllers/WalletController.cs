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
            var wallet = _unitOfWork.Wallet.GetLastWalletValue();

            return Ok(wallet.Balance);
        }

        // TODO: refactor this (first or default...)
        [HttpPost]
        public IActionResult Update([FromBody] Wallet wallet)
        {
            var lastWalletValue = _unitOfWork.Wallet.GetLastWalletValue();
            lastWalletValue.Balance = wallet.Balance;
            _unitOfWork.Wallet.Update(lastWalletValue);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
