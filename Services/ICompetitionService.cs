using System;
using System.Collections.Generic;
using BettingApp.Dtos;

namespace BettingApp.Services
{
    public interface ICompetitionService
    {
        IEnumerable<CompetitionSportsDto> GetCompetitionDetails();
    }
}
