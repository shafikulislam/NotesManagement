
using System.ComponentModel.DataAnnotations;

namespace NotesManagement.Api.DTOs.Notes
{
    public class NoteDto : BaseEntityDto
    {
        [MaxLength(100, ErrorMessage = "Note content cannot exceed 100 characters.")]
        public string? Content { get; set; } // Max length of 100
    }

}
