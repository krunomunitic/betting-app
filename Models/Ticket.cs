using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BettingApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int Stake { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
