using System;
using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class TicketDto
    {
        public int Stake { get; set; }
        public decimal OddsCalc { get; set; }

        public List<BetDto> Bets { get; set; }
    }
}
