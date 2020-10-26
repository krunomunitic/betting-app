using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(BettingAppContext context) : base(context)
        {
        }

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
