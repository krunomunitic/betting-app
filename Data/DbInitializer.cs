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

            var sports = new Sport[]
            {
                new Sport { Name = "Football" },
                new Sport { Name = "Basketball" },
                new Sport { Name = "American football" },
            };

            foreach (Sport sport in sports)
            {
                context.Sports.Add(sport);
            }
            context.SaveChanges();

            var competitions = new Competition[]
            {
                new Competition { Name = "Premier League", SportId = sports.Single(s=> s.Name == "Football").Id },
                new Competition { Name = "La Liga", SportId = sports.Single(s=> s.Name == "Football").Id },
                new Competition { Name = "Seria A", SportId = sports.Single(s=> s.Name == "Football").Id },
                new Competition { Name = "Champions League", SportId = sports.Single(s=> s.Name == "Football").Id },
                new Competition { Name = "NBA", SportId = sports.Single(s=> s.Name == "Basketball").Id },
                new Competition { Name = "Aba League", SportId = sports.Single(s=> s.Name == "Basketball").Id },
                new Competition { Name = "NFL", SportId = sports.Single(s=> s.Name == "American football").Id },
            };

            foreach (Competition competition in competitions)
            {
                context.Competitions.Add(competition);
            }
            context.SaveChanges();

            var teams = new Team[]
            {
                new Team { Name = "Arsenal" },
                new Team { Name = "Chelsea" },
                new Team { Name = "Manchester United" },
                new Team { Name = "Liverpool" },
                new Team { Name = "Everton" },
                new Team { Name = "Manchester City" },
                new Team { Name = "Real Madrid" },
                new Team { Name = "Barcelona" },
                new Team { Name = "Villareal" },
                new Team { Name = "Atletico Madrid" },
                new Team { Name = "Sevilla" },
                new Team { Name = "Juventus" },
                new Team { Name = "Lazio" },
                new Team { Name = "Inter" },
                new Team { Name = "Milan" },
                new Team { Name = "Bayern Munich" },
                new Team { Name = "LA Lakers" },
                new Team { Name = "Miami Heat" },
                new Team { Name = "Dallas Cowboys" },
                new Team { Name = "Seattle Seahawks" },
            };

            foreach (Team team in teams)
            {
                context.Teams.Add(team);
            }
            context.SaveChanges();


            var offers = new Offer[]
            {
                new Offer { Home = 1.20M, Away = 2.15M, Draw = 3.00M },
                new Offer { Home = 2.40M, Away = 4.15M, Draw = 1.80M },
                new Offer { Home = 5.80M, Away = 4.00M, Draw = 1.70M },
            };

            foreach (Offer offer in offers)
            {
                context.Offers.Add(offer);
            }
            context.SaveChanges();

            var fixtures = new Fixture[]
            {
                // TODO: set now plus a few days
                new Fixture { Date = DateTime.Parse("2020-11-11"), OfferId = 1, HomeTeamId = teams.Single(t=> t.Name == "Arsenal").Id, AwayTeamId = teams.Single(t => t.Name == "Chelsea").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Premier League").Id},
                new Fixture { Date = DateTime.Parse("2020-11-12"), OfferId = 2, HomeTeamId = teams.Single(t=> t.Name == "Manchester United").Id, AwayTeamId = teams.Single(t => t.Name == "Manchester City").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Premier League").Id},
                // TODO: set now minus few days, already played
                new Fixture { Date = DateTime.Parse("2020-10-10"), OfferId = 3, HomeTeamId = teams.Single(t=> t.Name == "Real Madrid").Id, AwayTeamId = teams.Single(t => t.Name == "Bayern Munich").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Champions League").Id, Result = "1:3"},
            };

            foreach (Fixture fixture in fixtures)
            {
                context.Fixtures.Add(fixture);
            }
            context.SaveChanges();
        }
    }
}
