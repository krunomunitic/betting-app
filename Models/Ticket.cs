using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BettingApp.ModelValidation;

namespace BettingApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "The Stake must be higher than 0")]
        public int Stake { get; set; }

        [TicketBetsValid]
        public List<Bet> Bets { get; set; }
    }
}
