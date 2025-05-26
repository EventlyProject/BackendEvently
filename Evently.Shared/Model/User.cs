using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public  string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<EventParticipant> EventParticipations { get; set; } = new List<EventParticipant>();
    }
}
