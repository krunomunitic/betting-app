﻿namespace BettingApp.Models
{
    public class FixtureOddsSpecial
    {
        public int FixtureId { get; set; }
        public Fixture Fixture { get; set; }

        public int OddsId { get; set; }
        public Odds Odds { get; set; }
    }
}
