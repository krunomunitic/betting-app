﻿using BettingApp.Models;
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
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>().ToTable("Bet");
            modelBuilder.Entity<Competition>().ToTable("Competition");
            modelBuilder.Entity<Fixture>().ToTable("Fixture");
            modelBuilder.Entity<Offer>().ToTable("Offer");
            modelBuilder.Entity<Result>().ToTable("Result");
            modelBuilder.Entity<Sport>().ToTable("Sport");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Wallet>().ToTable("Wallet");
        }
    }
}
