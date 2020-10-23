using System.Collections.Generic;
using System.Linq;
using BettingApp.Data;
using BettingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingApp.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(BettingAppContext context) : base(context)
        {
        }

        // TODO: select only whats needed
        public IEnumerable<Ticket> GetAllTicketsWithDetails()
        {
            return _context.Tickets
               .Include(t => t.Bets).ThenInclude(b => b.Odds)
               .Include(t => t.Bets).ThenInclude(b => b.Fixture)
               .ThenInclude(f => f.AwayTeam)
               .Include(t => t.Bets).ThenInclude(b => b.Fixture)
               .ThenInclude(f => f.HomeTeam);
        }
    }
}
