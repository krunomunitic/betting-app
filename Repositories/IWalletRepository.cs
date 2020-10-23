using BettingApp.Models;

namespace BettingApp.Repositories
{
    public interface IWalletRepository : IGenericRepository <Wallet>
    {
        Wallet GetLastWalletValue();
    }
}
