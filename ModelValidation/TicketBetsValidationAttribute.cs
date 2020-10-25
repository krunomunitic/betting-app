using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BettingApp.Models;

namespace BettingApp.ModelValidation
{
    public class TicketBetsValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            IList<Bet> bets = value as IList<Bet>;

            if (bets.Count == 0)
            {
                return false;
            }

            // check only fixtures,
            // because of the idea that odds could be reused
            if (bets.GroupBy(b => b.FixtureId).Count() != bets.Count())
            {
                return false;
            }

            return true;
        }
    }
}
