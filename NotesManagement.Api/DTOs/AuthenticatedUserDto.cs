namespace NotesManagement.Api.DTOs
{
    public class AuthenticatedUserDto : RegisterDto
    {
        public required string Token { get; set; }
    }
}
