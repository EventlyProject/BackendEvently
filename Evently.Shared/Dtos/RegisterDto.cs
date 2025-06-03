namespace BackendEvently.Dtos
{
    // Data Transfer Object for user registration
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Emailaddress {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
