using System.Collections.Generic;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public interface ICompetitionRepository : IGenericRepository<Competition>
    {
        IEnumerable<Competition> GetCompetitionsBySports();
    }
}
