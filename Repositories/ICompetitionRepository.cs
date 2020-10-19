using System.Collections.Generic;
using BettingApp.Models;
using System.Linq;

namespace BettingApp.Repositories
{
    public interface ICompetitionRepository : IGenericRepository<Competition>
    {
        IEnumerable<Competition> GetCompetitionsBySports();
    }
}
