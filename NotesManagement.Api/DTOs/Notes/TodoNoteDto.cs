namespace NotesManagement.Api.DTOs.Notes
{
    public class TodoNoteDto : NoteDto
    {
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
