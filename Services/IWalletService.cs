using BettingApp.Models;

namespace BettingApp.Services
{
    public interface IWalletService
    {
        public int GetWalletBalance();
        public int UpdateWallet(Wallet wallet);
    }
}
