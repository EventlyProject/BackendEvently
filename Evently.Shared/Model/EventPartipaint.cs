namespace BackendEvently.Model
{
    public class EventPartipaint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int EventId {  get; set; }
        public Event? Event { get; set; }
        public DateTime RegisteretAt { get; set; } = DateTime.Now;

    }
}
