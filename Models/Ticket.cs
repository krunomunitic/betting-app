using System;
using System.Collections.Generic;

namespace BettingApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
