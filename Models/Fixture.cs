using System;
namespace BettingApp.Models
{
    public class Fixture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public Team Team { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public int ResultId { get; set; }
        public Result Result { get; set; }
    }
}
