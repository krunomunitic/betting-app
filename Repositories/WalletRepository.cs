using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(BettingAppContext context) : base(context)
        {
        }
    }
}
