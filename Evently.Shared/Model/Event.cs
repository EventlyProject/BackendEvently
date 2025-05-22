using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string logoUrl { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public decimal? Price { get; set; }
        public string AccessRequirements { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<EventPartipaint> Participants { get; set; }
    }
}
