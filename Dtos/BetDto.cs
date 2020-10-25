using System;

namespace BettingApp.Dtos
{
    public class BetDto
    {
        public DateTime FixtureDate { get; set; }

        public string OddsName { get; set; }
        public decimal? OddsValue { get; set; }

        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }

        public string Result { get; set; }
    }
}
