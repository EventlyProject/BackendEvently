using System.ComponentModel.DataAnnotations;

namespace BackendEvently.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public  string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Emailaddress { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<EventPartipaint> EventParticipations { get; set; } = new List<EventPartipaint>();
    }
}
