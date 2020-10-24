using BettingApp.Models;

namespace BettingApp.Services
{
    public interface IWalletService
    {
        public decimal GetWalletBalance();
        public int UpdateWallet(Wallet wallet);
    }
}
