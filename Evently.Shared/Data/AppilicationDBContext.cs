using BackendEvently.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Data
{
    public class AppilicationDBContext : DbContext
    {
        public AppilicationDBContext(DbContextOptions<AppilicationDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventPartipaint> EventParticipants { get; set; }
        public DbSet<Category> Categoryes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventPartipaint>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EventParticipations)
                .HasForeignKey(ep => ep.UserId);

            modelBuilder.Entity<EventPartipaint>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId);
        }
    }
}
