using System.ComponentModel.DataAnnotations;

namespace NotesManagement.Api.DTOs
{
    public class RegisterDto
    {
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public required string Password { get; set; }
    }

}
