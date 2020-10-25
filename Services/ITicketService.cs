using System.Collections.Generic;
using BettingApp.Dtos;
using BettingApp.Models;

namespace BettingApp.Services
{
    public interface ITicketService
    {
        public IEnumerable<TicketDto> GetTickets();
        public bool ValidateTicketBets(IEnumerable<Bet> bets);
        public bool ValidateTicketStake(int stake);
        public void CreateTicket(Ticket ticket);
    }
}
