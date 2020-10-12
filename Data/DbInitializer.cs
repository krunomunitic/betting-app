using System;
using BettingApp.Models;

namespace BettingApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BettingAppContext context)
        {
            var wallet = new Wallet { Balance = 100 };
            context.Wallets.Add(wallet);

            context.SaveChanges();
        }
    }
}
