using System;
using BettingApp.Repositories;

namespace BettingApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFixtureRepository Fixtures { get; }
        ICompetitionRepository Competition { get; }
        IWalletRepository Wallet { get; }
        ITicketRepository Ticket { get; }
        IBetRepository Bet { get; }
        IFixtureOddsSpecialRepository FixtureOddsSpecial { get; }
        IOddsRepository Odds { get; }

        int Complete();
    }
}
