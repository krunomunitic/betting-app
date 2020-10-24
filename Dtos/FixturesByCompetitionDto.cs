using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class FixturesByCompetitionDto
    {
        public string CompetitionName { get; set; }
        public IList<FixtureDto> Fixtures { get; set; }
    }
}
