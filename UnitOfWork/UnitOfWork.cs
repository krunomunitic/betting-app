﻿using System;
using BettingApp.Data;
using BettingApp.Repositories;

namespace BettingApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BettingAppContext _context;

        public IFixtureRepository Fixtures { get; private set; }
        public ICompetitionRepository Competition { get; private set; }
        public IWalletRepository Wallet { get; private set; }

        public UnitOfWork(BettingAppContext context)
        {
            _context = context;
            Fixtures = new FixtureRepository(_context);
            Competition = new CompetitionRepository(_context);
            Wallet = new WalletRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
