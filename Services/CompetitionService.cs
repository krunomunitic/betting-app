using System.Linq;
using System.Collections.Generic;
using BettingApp.UnitOfWork;
using BettingApp.Dtos;
using BettingApp.Models;

namespace BettingApp.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompetitionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CompetitionSportsDto> GetCompetitionDetails()
        {
            IEnumerable<Competition> competitionsBySports = _unitOfWork.Competition.GetCompetitionsBySports();

            return competitionsBySports.GroupBy(c => c.Sport)
                .Select(c => new CompetitionSportsDto
                {
                    SportName = c.Key.Name,
                    Competitions = c.Select(c => new CompetitionDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }).ToList();
        }
    }
}
