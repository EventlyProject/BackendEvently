using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEvently.Model
{
    public class EventPartipaint
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int EventId {  get; set; }
        public Event? Event { get; set; }
        public DateTime RegisteretAt { get; set; } = DateTime.Now;

    }
}
