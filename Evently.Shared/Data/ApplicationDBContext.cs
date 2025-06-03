using BackendEvently.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendEvently.Data
{
    // The application's Entity Framework Core database context
    public class ApplicationDBContext : DbContext
    {
        // Constructor that passes options to the base DbContext
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        // Table for users
        public DbSet<User> Users { get; set; }
        // Table for events
        public DbSet<Event> Events { get; set; }
        // Table for event participants (join table between User and Event)
        public DbSet<EventPartipaint> EventParticipants { get; set; }
        // Table for categories
        public DbSet<Category> Categoryes { get; set; }
        // Configure entity relationships and property settings
        protected override void OnModelCreating(ModelBuilder modelSBuilder)
        {
            // Each EventPartipaint is linked to one User (many participations per user)
            modelSBuilder.Entity<EventPartipaint>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EventParticipations)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Cascade);// Deleting a user deletes their participations

            // Each EventPartipaint is linked to one Event (many participations per event)
            modelSBuilder.Entity<EventPartipaint>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Restrict);// Deleting an event does NOT delete participations

            // Configure the Price property on Event to be a nullable decimal with precision
            modelSBuilder.Entity<Event>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

        }
    }
}
