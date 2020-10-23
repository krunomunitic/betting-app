using System.Collections.Generic;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        IEnumerable<Ticket> GetAllTicketsWithDetails();
    }
}
