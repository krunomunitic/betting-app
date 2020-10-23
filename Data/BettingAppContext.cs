using System.Linq;
using BettingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingApp.Data
{
    public class BettingAppContext : DbContext
    {
        public BettingAppContext(DbContextOptions<BettingAppContext> options): base(options)
        {
        }

        public DbSet<Bet> Betts { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Odds> Odds { get; set; }
        public DbSet<FixtureOdds> FixtureOdds { get; set; }
        public DbSet<FixtureOddsSpecial> FixtureOddsSpecial { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>().ToTable("Bet");
            modelBuilder.Entity<Competition>().ToTable("Competition");
            modelBuilder.Entity<Fixture>().ToTable("Fixture");
            modelBuilder.Entity<Sport>().ToTable("Sport");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Wallet>().ToTable("Wallet");

            modelBuilder.Entity<FixtureOdds>()
                .HasKey(fo => new { fo.FixtureId, fo.OddsId });

            modelBuilder.Entity<FixtureOddsSpecial>()
                .HasKey(fo => new { fo.FixtureId, fo.OddsId });

            modelBuilder.Entity<Wallet>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // TODO: temp fix, do this for each model separately
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}
