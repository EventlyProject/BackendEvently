using System.Text.Json.Serialization;

namespace BackendEvently.Dtos
{
    public class EventDto
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public string Details { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public decimal? Price { get; set; }
        public string AccessRequirements {  get; set; } = string.Empty;
        public int CategoryId {  get; set; }
        [JsonIgnore]
        public string CategoryName { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
