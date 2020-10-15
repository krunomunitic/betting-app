using System;
using BettingApp.Repositories;

namespace BettingApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFixtureRepository Fixtures { get; }
        ICompetitionRepository Competition { get; }
        IWalletRepository Wallet { get; }
        int Complete();
    }
}
