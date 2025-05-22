using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        public ICollection<EventPartipaint> EventParticipations { get; set; }
    }
}
