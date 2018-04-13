using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinballTourneyApp.Models;
using Microsoft.EntityFrameworkCore;


namespace PinballTourneyApp.Data
{
    public class PinballDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        public PinballDbContext(DbContextOptions<PinballDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TournamentUser>()
                .HasKey(c => new { c.TournamentID, c.UserID });
        }
    }
}
