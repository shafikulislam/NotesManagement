using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NotesManagement.Api.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public required string HashedPassword { get; set; }
    }
}
