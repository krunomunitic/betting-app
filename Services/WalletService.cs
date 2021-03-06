﻿using BettingApp.UnitOfWork;
using BettingApp.Models;

namespace BettingApp.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal GetWalletBalance()
        {
            // TODO: what if somehow there isn't any wallets in db
            Wallet wallet = _unitOfWork.Wallet.GetLastWalletValue();
            return wallet.Balance;
        }

        public void UpdateWallet(Wallet wallet)
        {
            // TODO: what if somehow there isn't any wallets in db
            Wallet lastWalletValue = _unitOfWork.Wallet.GetLastWalletValue();

            lastWalletValue.Balance = wallet.Balance;
            _unitOfWork.Wallet.Update(lastWalletValue);

            _unitOfWork.Complete();
        }
    }
}
