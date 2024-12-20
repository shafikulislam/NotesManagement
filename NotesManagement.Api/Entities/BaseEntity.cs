using System.ComponentModel.DataAnnotations;

namespace NotesManagement.Api.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
