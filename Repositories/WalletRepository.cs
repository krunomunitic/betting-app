using System.Linq;
using BettingApp.Data;
using BettingApp.Models;

namespace BettingApp.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(BettingAppContext context) : base(context)
        {
        }

        public Wallet GetLastWalletValue()
        {
            return _context.Wallets.OrderByDescending(w => w.CreatedDate).First();
        }
    }
}
