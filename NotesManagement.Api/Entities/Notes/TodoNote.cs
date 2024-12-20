namespace NotesManagement.Api.Entities.Notes
{
    public class TodoNote : Note
    {
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
