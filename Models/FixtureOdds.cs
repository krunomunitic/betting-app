using System;

namespace BettingApp.Models
{
    public class FixtureOdds
    {
        public int FixtureId { get; set; }
        public Fixture Fixture { get; set; }
        public int OddsId { get; set; }
        public Odds Odds { get; set; }
    }
}
