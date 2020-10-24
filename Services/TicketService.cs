using System.Linq;
using System.Collections.Generic;
using BettingApp.UnitOfWork;
using BettingApp.Dtos;
using BettingApp.Models;

namespace BettingApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TicketDto> GetTickets()
        {
            var tickets = _unitOfWork.Ticket.GetAllTicketsWithDetails();

            return tickets.Select(t => new TicketDto
            {
                Stake = t.Stake,
                OddsCalc = t.Bets.Aggregate((decimal)1, (total, value) => total * value.Odds.Value ?? 1),
                Bets = t.Bets.Select(b => new BetDto
                {
                    FixtureDate = b.Fixture.Date,
                    OddsName = b.Odds.Name,
                    OddsValue = b.Odds.Value,
                    HomeTeamId = b.Fixture.HomeTeam.Id,
                    HomeTeamName = b.Fixture.HomeTeam.Name,
                    AwayTeamId = b.Fixture.AwayTeam.Id,
                    AwayTeamName = b.Fixture.AwayTeam.Name,
                    Result = b.Fixture.Result
                }).ToList(),
            }).ToList();
        }

        public int CreateTicket(Ticket ticket)
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

            return 1;
        }

        /*[HttpPost]
        public IActionResult Create(Ticket ticket)
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
        }*/
    }
}
