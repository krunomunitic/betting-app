using BettingApp.UnitOfWork;
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

        public int GetWalletBalance()
        {
            var wallet = _unitOfWork.Wallet.GetLastWalletValue();
            return wallet.Balance;
        }

        public int UpdateWallet(Wallet wallet)
        {
            var lastWalletValue = _unitOfWork.Wallet.GetLastWalletValue();

            lastWalletValue.Balance = wallet.Balance;
            _unitOfWork.Wallet.Update(lastWalletValue);

            _unitOfWork.Complete();

            return lastWalletValue.Id;
        }
    }
}
