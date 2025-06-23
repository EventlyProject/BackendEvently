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


            //seed data category
            modelSBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "workshop" },
                new Category { Id = 1, Name = "Foredrag" },
                new Category { Id = 1, Name = "Sport" },
                new Category { Id = 1, Name = "Musik" },
                new Category { Id = 1, Name = "Kunst" },
                new Category { Id = 1, Name = "Netwærk" },
                new Category { Id = 1, Name = "Teknologi" },
                new Category { Id = 1, Name = "Sundhed" },
                new Category { Id = 1, Name = "Mad & Drikke" },
                new Category { Id = 1, Name = "Turnering" }
            );
            //seed data event
            modelSBuilder.Entity<Event>().HasData(
new Event { Id = 1, Name = "C# Workshop", LogoUrl = "", StartTime = DateTime.Now.AddDays(7), Details = "Lær C#",
    Location = "Room 101", MaxParticipants = 30, Price = 0, AccessRequirements = "", CategoryId = 1, UserId = 1 },
            new Event { Id = 2, Name = "Sommerfest", LogoUrl = "", StartTime = DateTime.Now.AddDays(14), Details = "Sommerfest for alle",
                Location = "Café", MaxParticipants = 100, Price = 50, AccessRequirements = "", CategoryId = 2, UserId = 1 },
            new Event { Id = 3, Name = "Foredrag om AI", LogoUrl = "", StartTime = DateTime.Now.AddDays(10), Details = "Bliv klogere på AI",
                Location = "Auditorium", MaxParticipants = 80, Price = 0, AccessRequirements = "", CategoryId = 7, UserId = 1 },
            new Event { Id = 4, Name = "Yoga i parken", LogoUrl = "", StartTime = DateTime.Now.AddDays(5), Details = "Morgenyoga",
                Location = "Byparken", MaxParticipants = 20, Price = 0, AccessRequirements = "", CategoryId = 8, UserId = 1 },
            new Event { Id = 5, Name = "Netværksaften", LogoUrl = "", StartTime = DateTime.Now.AddDays(12), Details = "Mød nye mennesker",
                Location = "Lounge", MaxParticipants = 40, Price = 0, AccessRequirements = "", CategoryId = 6, UserId = 1 },
            new Event { Id = 6, Name = "Madlavningskursus", LogoUrl = "", StartTime = DateTime.Now.AddDays(8), Details = "Lær at lave mad",
                Location = "Køkkenet", MaxParticipants = 15, Price = 100, AccessRequirements = "", CategoryId = 9, UserId = 1 },
            new Event { Id = 7, Name = "Kunstudstilling", LogoUrl = "", StartTime = DateTime.Now.AddDays(20), Details = "Se moderne kunst",
                Location = "Galleri", MaxParticipants = 60, Price = 25, AccessRequirements = "", CategoryId = 5, UserId = 1 },
            new Event { Id = 8, Name = "Fodboldturnering", LogoUrl = "", StartTime = DateTime.Now.AddDays(15), Details = "Turnering for alle",
                Location = "Stadion", MaxParticipants = 22, Price = 0, AccessRequirements = "", CategoryId = 3, UserId = 1 },
            new Event { Id = 9, Name = "Musikfestival", LogoUrl = "", StartTime = DateTime.Now.AddDays(30), Details = "Live musik",
                Location = "Pladsen", MaxParticipants = 200, Price = 150, AccessRequirements = "", CategoryId = 4, UserId = 1 },
            new Event { Id = 10, Name = "Andet Event", LogoUrl = "", StartTime = DateTime.Now.AddDays(18), Details = "Diverse aktiviteter",
                Location = "Fælleshuset", MaxParticipants = 50, Price = 0, AccessRequirements = "", CategoryId = 10, UserId = 1 }
             );
            // seed data user
            modelSBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Emailaddress = "admin@evently.com", PasswordHash = "admin1234", Role = "admin" },
                new User { Id = 2, Username = "alice", Emailaddress = "alice@example.com", PasswordHash = "password1", Role = "user" },
                new User { Id = 3, Username = "bob", Emailaddress = "bob@example.com", PasswordHash = "password2", Role = "user" },
                new User { Id = 4, Username = "carol", Emailaddress = "carol@example.com", PasswordHash = "password3", Role = "user" },
                new User { Id = 5, Username = "dave", Emailaddress = "dave@example.com", PasswordHash = "password4", Role = "user" },
                new User { Id = 6, Username = "eve", Emailaddress = "eve@example.com", PasswordHash = "password5", Role = "user" }
            );
        }

    }
}
