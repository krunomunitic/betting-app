using System;
using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class FixtureDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string SportName { get; set; }
        public string CompetitionName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string Result { get; set; }

        public Dictionary<string, OddsDto> Odds { get; set; }
    }
}
