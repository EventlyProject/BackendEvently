namespace BackendEvently.Dtos
{
    // Data Transfer Object for user login
    public class LoginDto
    {
        public string Emailaddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
