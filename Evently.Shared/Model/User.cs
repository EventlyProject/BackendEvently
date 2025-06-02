using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public string Role { get; set; } = "User";
        [JsonIgnore]
        public ICollection<EventPartipaint> EventParticipations { get; set; } = new List<EventPartipaint>();
    }
}
