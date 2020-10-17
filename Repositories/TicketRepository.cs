using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(BettingAppContext context) : base(context)
        {
        }
    }
}
