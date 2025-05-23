using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string logoUrl { get; set; } = string.Empty;
        [Required]
        public DateTime StartTime { get; set; }
        public string Details { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public decimal? Price { get; set; }
        public string AccessRequirements { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<EventPartipaint> Participants { get; set; } = new List<EventPartipaint>();
    }
}
