using System;
using System.Linq;
using BettingApp.Models;

namespace BettingApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BettingAppContext context)
        {
            if (context.Wallets.Any())
            {
                return;
            }

            var wallet = new Wallet { Balance = 100 };
            context.Wallets.Add(wallet);

            context.SaveChanges();
        }
    }
}
