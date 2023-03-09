using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Models;

namespace TicTacToe.DAL;

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Game>().ToTable("games");
        builder.Entity<Game>().HasKey(k => k.Id);
        builder.Entity<Game>().Property(p => p.Id).HasColumnName("id");
        builder.Entity<Game>().Property(p => p.Table).HasColumnName("table").IsRequired();
        builder.Entity<Game>().Property(p => p.Winner).HasColumnName("winner");
        builder.Entity<Game>().Property(p => p.LastStep).HasColumnName("last_step");
    }
}
