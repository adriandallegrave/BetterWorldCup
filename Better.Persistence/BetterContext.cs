using Better.Domain.Models;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Better.Persistence
{
    public class BetterContext : IdentityDbContext
    {
        public BetterContext(DbContextOptions<BetterContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warning => warning.Ignore(CoreEventId.MultipleNavigationProperties));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Account

            modelBuilder.Entity<Account>()
                .ToTable("Accounts")
                .HasKey(account => account.Id);
            modelBuilder.Entity<Account>()
                .Property(account => account.Email)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Account>()
                .Property(account => account.HaveBets)
                .HasDefaultValue(false);
            modelBuilder.Entity<Account>()
                .Property(account => account.Balance)
                .HasDefaultValue(0.00)
                .HasColumnType("money");
            modelBuilder.Entity<Account>()
                .Property(account => account.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Transaction

            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions")
                .HasKey(transaction => transaction.Id);
            modelBuilder.Entity<Transaction>()
                .Property(transaction => transaction.Amount)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<Transaction>()
                .Property(game => game.Timestamp)
                .HasColumnType("datetime2")
                .IsRequired(true);

            // Bet

            modelBuilder.Entity<Bet>()
                .ToTable("Bets")
                .HasKey(bet => bet.Id);

            // Team

            modelBuilder.Entity<Team>()
                .ToTable("Teams")
                .HasKey(team => team.Id);
            modelBuilder.Entity<Team>()
                .Property(team => team.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Team>()
                .Property(team => team.ShortName)
                .HasColumnType("nvarchar")
                .HasMaxLength(3)
                .IsRequired();
            modelBuilder.Entity<Team>()
                .Property(team => team.SourceName)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            // Game

            modelBuilder.Entity<Game>()
                .ToTable("Games")
                .HasKey(game => game.Id);
            modelBuilder.Entity<Game>()
                .Property(game => game.HomeScored)
                .HasColumnType("int")
                .HasDefaultValue(-1);
            modelBuilder.Entity<Game>()
                .Property(game => game.AwayScored)
                .HasColumnType("int")
                .HasDefaultValue(-1);
            modelBuilder.Entity<Game>()
                .Property(game => game.StartTime)
                .HasColumnType("datetime2")
                .IsRequired(true);
            modelBuilder.Entity<Game>()
                .HasOne(game => game.HomeTeam)
                .WithMany(game => game.HomeGames)
                .HasForeignKey(game => game.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Game>()
                .HasOne(game => game.AwayTeam)
                .WithMany(game => game.AwayGames)
                .HasForeignKey(game => game.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            // Inserts

            modelBuilder.Entity<Account>()
                .HasData(new Account
                {
                    Id = Guid.Parse(Helpers.GuidValue("0101")),
                    Name = "Adrian",
                    Email = "adriandallegrave@gmail.com",
                    HaveBets = false,
                    Balance = 0
                });

            var teams = Helpers.GetListOfTeams();

            for (var i = 0; i < teams.Count; i++)
            {
                modelBuilder.Entity<Team>()
                    .HasData(new Team
                    {
                        Id = Guid.Parse(Helpers.GuidValue(teams[i][0])),
                        Name = teams[i][1],
                        ShortName = teams[i][2],
                        SourceName = teams[i][3]
                    });
            }

            var games = Helpers.GetListOfMatches();

            for (var i = 0; i < games.Count; i++)
            {
                modelBuilder.Entity<Game>()
                    .HasData(new Game
                    {
                        Id = Guid.Parse(Helpers.GuidValue((string)games[i][0])),
                        HomeScored = -1,
                        AwayScored = -1,
                        StartTime = (DateTime)games[i][1],
                        HomeTeamId = Guid.Parse(Helpers.GuidValue((string)games[i][2])),
                        AwayTeamId = Guid.Parse(Helpers.GuidValue((string)games[i][3])),
                    });
            }
        }
    }
}
