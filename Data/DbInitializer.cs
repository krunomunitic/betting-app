using System;
using System.Linq;
using System.Collections.Generic;
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


            var odds = new Odds[]
            {
                new Odds { Name = "1", Value = 2.15M },
                new Odds { Name = "2", Value = 2.05M },
                new Odds { Name = "X", Value = 3.75M },
                new Odds { Name = "12", Value = 1.30M },
                new Odds { Name = "X1", Value = 1.60M },
                new Odds { Name = "X2", Value = 1.70M },

                new Odds { Name = "1", Value = 1.80M },
                new Odds { Name = "2", Value = 3.50M },
                new Odds { Name = "X", Value = 2.90M },
                new Odds { Name = "12", Value = 1.25M },
                new Odds { Name = "X1", Value = 1.40M },
                new Odds { Name = "X2", Value = 1.85M },

                new Odds { Name = "1", Value = 4.55M },
                new Odds { Name = "2", Value = 1.30M },
                new Odds { Name = "X", Value = 2.75M },
                new Odds { Name = "12", Value = 1.15M },
                new Odds { Name = "X1", Value = 2.00M },
                new Odds { Name = "X2", Value = 1.10M },

                new Odds { Name = "1", Value = 1.25M },
                new Odds { Name = "2", Value = 5.45M },
                new Odds { Name = "X", Value = 3.00M },
                new Odds { Name = "12", Value = 1.10M },
                new Odds { Name = "X1", Value = 1.15M },
                new Odds { Name = "X2", Value = 2.65M },
            };

            foreach (Odds singleOdds in odds)
            {
                context.Odds.Add(singleOdds);
            }
            context.SaveChanges();

            var fixtures = new Fixture[]
            {
                // TODO: set now plus a few days
                new Fixture { Date = DateTime.Parse("2020-11-11"), HomeTeamId = teams.Single(t=> t.Name == "Arsenal").Id, AwayTeamId = teams.Single(t => t.Name == "Chelsea").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Premier League").Id},
                new Fixture { Date = DateTime.Parse("2020-11-12"), HomeTeamId = teams.Single(t=> t.Name == "Manchester United").Id, AwayTeamId = teams.Single(t => t.Name == "Manchester City").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Premier League").Id},
                // TODO: set now minus few days, already played
                new Fixture { Date = DateTime.Parse("2020-10-10"), HomeTeamId = teams.Single(t=> t.Name == "Real Madrid").Id, AwayTeamId = teams.Single(t => t.Name == "Bayern Munich").Id,  CompetitionId =  competitions.Single(s=> s.Name == "Champions League").Id, Result = "1:3"},
            };

            foreach (Fixture fixture in fixtures)
            {
                context.Fixtures.Add(fixture);
            }
            context.SaveChanges();

            int startIndex = 0;
            foreach (Fixture fixture in fixtures)
            {
                for (int i = startIndex; i < startIndex + 6; i++, startIndex++)
                {
                    if (startIndex == 16) continue;
                    if (startIndex == 18) continue;

                    var fixtureOdds = new FixtureOdds
                    {
                        FixtureId = fixture.Id,
                        OddsId = odds[i].Id
                    };

                    context.FixtureOdds.Add(fixtureOdds);
                }
            }
            context.SaveChanges();
        }
    }
}
