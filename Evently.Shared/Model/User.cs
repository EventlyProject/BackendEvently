using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendEvently.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; } // Primary key for the User entity

        [Required]
        public string Username { get; set; } = string.Empty; // Username of the user (required)

        [Required]
        [EmailAddress]
        public string Emailaddress { get; set; } = string.Empty; // Email address of the user (required, must be a valid email)

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Hashed password of the user (required)

        [JsonIgnore]
        public string Role { get; set; } = "User"; // Role of the user, default is "User", ignored in JSON serialization

        [JsonIgnore]
        public ICollection<EventPartipaint> EventParticipations { get; set; } = new List<EventPartipaint>(); // List of event participations for the user, ignored in JSON serialization
    }
}
