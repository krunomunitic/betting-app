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
    public class TicketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _unitOfWork.Ticket.GetAllTicketsWithDetails();

            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Ticket ticket)
        {
            var bets = new List<Bet>();
            ticket.Bets.ForEach(b =>
            {
                var bet = new Bet { FixtureId = b.FixtureId, OddsId = b.OddsId };
                bets.Add(bet);
                _unitOfWork.Bet.Insert(bet);
            });

            _unitOfWork.Ticket.Insert(new Ticket { Bets = bets, Stake = ticket.Stake });

            var wallet = _unitOfWork.Wallet.GetLastWalletValue();
            var newWallet = new Wallet
            {
                Balance = wallet.Balance - ticket.Stake
            };
            _unitOfWork.Wallet.Insert(newWallet);

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
