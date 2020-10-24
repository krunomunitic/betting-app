using System.Collections.Generic;
using BettingApp.Dtos;
using BettingApp.Models;

namespace BettingApp.Services
{
    public interface ITicketService
    {
        public IEnumerable<TicketDto> GetTickets();
        public int CreateTicket(Ticket ticket);
    }
}
