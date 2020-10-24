using System.Collections.Generic;

namespace BettingApp.Dtos
{
    public class CompetitionSportsDto
    {
        public string SportName { get; set; }
        public IList<CompetitionDto> Competitions { get; set; }
    }
}
