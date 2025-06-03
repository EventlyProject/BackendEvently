using BackendEvently.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEvently.Model
{
    // Represents an event in the system
    public class Event
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the event

        [Required]
        public string Name { get; set; } = string.Empty; // Name of the event

        public string LogoUrl { get; set; } = string.Empty; // URL to the event's logo image

        [Required]
        public DateTime StartTime { get; set; } // Date and time when the event starts

        public string Details { get; set; } = string.Empty; // Additional details about the event

        public string Location { get; set; } = string.Empty; // Location where the event takes place

        public int MaxParticipants { get; set; } // Maximum number of participants allowed

        public decimal? Price { get; set; } // Price to attend the event (nullable)

        public string AccessRequirements { get; set; } = string.Empty; // Requirements to access the event

        public int CategoryId { get; set; } // Foreign key for the event's category

        public Category? Category { get; set; } // Navigation property for the event's category

        public ICollection<EventPartipaint> Participants { get; set; } = new List<EventPartipaint>(); // List of participants in the event

        public int UserId { get; set; } // Foreign key for the user who created the event

        [ForeignKey("UserId")]
        public User? User { get; set; } // Navigation property for the event's creator
    }
}
