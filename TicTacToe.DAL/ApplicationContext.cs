using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain.Models;

namespace TicTacToe.DAL
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>().ToTable("games");
            builder.Entity<Game>().HasKey(k => k.Id);
            builder.Entity<Game>().Property(p => p.Id).HasColumnName("id");
            //builder.Entity<Game>().Property(p => p.FirstPlayer).HasColumnName("first_player").IsRequired();
            //builder.Entity<Game>().Property(p => p.SecondPlayer).HasColumnName("second_player").IsRequired();
            builder.Entity<Game>().Property(p => p.Table).HasColumnName("table").IsRequired();
            builder.Entity<Game>().Property(p => p.Winner).HasColumnName("winner");
            builder.Entity<Game>().Property(p => p.LastStep).HasColumnName("last_step");
        }
    }
}
