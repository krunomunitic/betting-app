using BettingApp.UnitOfWork;
using System.Collections.Generic;
using BettingApp.Dtos;
using System.Linq;

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
            var _competitionsBySports = _unitOfWork.Competition.GetCompetitionsBySports();

            // TODO: add automapper
            var competitionsBySports = _competitionsBySports.GroupBy(c => c.Sport)
                .Select(c => new CompetitionSportsDto
                {
                    SportName = c.Key.Name,
                    Competitions = c.Select(c => new CompetitionDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }).ToList();

            return competitionsBySports;
        }
    }
}
