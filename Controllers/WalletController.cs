using Microsoft.AspNetCore.Mvc;
using BettingApp.Services;
using BettingApp.Models;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            decimal balance = _walletService.GetWalletBalance();

            return Ok(balance);
        }

        [HttpPost]
        public IActionResult Update([FromBody] Wallet wallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _walletService.UpdateWallet(wallet);

            return Ok();
        }
    }
}
