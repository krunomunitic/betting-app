using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class FixturesDto
    {
        public IList<FixturesByCompetitionDto> FixturesByCompetition { get; set; }
        public IList<FixturesByCompetitionDto> FixturesByCompetitionSpecial { get; set; }
    }
}
