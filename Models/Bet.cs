namespace BettingApp.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public int OddsId { get; set; }
        public Odds Odds { get; set; }

        public int FixtureId { get; set; }
        public Fixture Fixture { get; set; }
    }
}
