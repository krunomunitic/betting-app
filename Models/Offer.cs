using System;

namespace BettingApp.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public decimal Home { get; set; }
        public decimal Draw { get; set; }
        public decimal Away { get; set; }
    }
}
