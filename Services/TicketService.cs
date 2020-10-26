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
            IEnumerable<Ticket> tickets = _unitOfWork.Ticket.GetAllTicketsWithDetails();

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

        public void CreateTicket(Ticket ticket)
        {
            List<Bet> bets = new List<Bet>();
            ticket.Bets.ForEach(b =>
            {
                Bet bet = new Bet
                { FixtureId = b.FixtureId, OddsId = b.OddsId };

                bets.Add(bet);
                _unitOfWork.Bet.Insert(bet);
            });

            _unitOfWork.Ticket
                .Insert(new Ticket { Bets = bets, Stake = ticket.Stake });

            Wallet wallet = _unitOfWork.Wallet.GetLastWalletValue();
            Wallet newWallet = new Wallet
            {
                Balance = wallet.Balance - (decimal)(ticket.Stake * 1.05)
            };

            _unitOfWork.Wallet.Insert(newWallet);

            _unitOfWork.Complete();
        }

        public bool ValidateTicketBets(IEnumerable<Bet> bets)
        {
            int betsWithSpecialOddsCount = bets.Where(
                b => _unitOfWork.FixtureOddsSpecial.Find(
                    fo => fo.FixtureId == b.FixtureId && fo.OddsId == b.OddsId)
                .Any()).Count();

            if (betsWithSpecialOddsCount == 0)
            {
                return true;
            }

            if (betsWithSpecialOddsCount > 1)
            {
                return false;
            }

            int betsCount = bets.Count();

            if (betsCount < 6)
            {
                return false;
            }

            int validBetsCount = bets.Where(b => !_unitOfWork.FixtureOddsSpecial.Find(
                fo => fo.FixtureId == b.FixtureId && fo.OddsId == b.OddsId)
            .Any() && _unitOfWork.Odds.FindById(b.OddsId).Value > (decimal)1.1).Count();

            if (validBetsCount < betsCount - 1)
            {
                return false;
            }

            return true;
        }

        public bool ValidateTicketStake(int stake)
        {
            Wallet wallet = _unitOfWork.Wallet.GetLastWalletValue();

            return wallet.Balance >= stake;
        }
    }
}
