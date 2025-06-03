using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEvently.Model
{
    // Represents a participant in an event
    public class EventPartipaint
    {
        // Primary key
        [Key]
        public int Id { get; set; }

        // Foreign key to User
        public int UserId { get; set; }

        // Navigation property to User
        [ForeignKey("UserId")]
        public User? User { get; set; }

        // Foreign key to Event
        public int EventId { get; set; }

        // Navigation property to Event
        public Event? Event { get; set; }

        // Date and time when the user registered for the event
        public DateTime RegisteretAt { get; set; } = DateTime.Now;
    }
}
