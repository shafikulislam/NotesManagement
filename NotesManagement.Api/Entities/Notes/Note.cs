using System.ComponentModel.DataAnnotations;

namespace NotesManagement.Api.Entities.Notes
{
    public abstract class Note : BaseEntity
    {

        [MaxLength(100, ErrorMessage = "Note content cannot exceed 100 characters.")]
        public string? Content { get; set; } // Max length: 100
    }

}
