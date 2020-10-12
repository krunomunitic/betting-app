using System;
namespace BettingApp.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public int Outcome { get; set; }

        public int FixtureId { get; set; }
        public Fixture Fixture { get; set; }
    }
}
