
using NotesManagement.Api.DTOs.Notes;

namespace NotesManagement.Api.Services.Interfaces
{
    public interface INoteService
    {
        // Create
        Task<RegularNoteDto> CreateRegularNoteAsync(RegularNoteDto note);
        Task<ReminderNoteDto> CreateReminderNoteAsync(ReminderNoteDto note);
        Task<TodoNoteDto> CreateTodoNoteAsync(TodoNoteDto note);
        Task<BookmarkNoteDto> CreateBookmarkNoteAsync(BookmarkNoteDto note);

        // Update
        Task<RegularNoteDto> UpdateRegularNoteAsync(RegularNoteDto note);
        Task<ReminderNoteDto> UpdateReminderNoteAsync(ReminderNoteDto note);
        Task<TodoNoteDto> UpdateTodoNoteAsync(TodoNoteDto note);
        Task<BookmarkNoteDto> UpdateBookmarkNoteAsync(BookmarkNoteDto note);

        // Delete
        Task DeleteRegularNoteAsync(string id);
        Task DeleteReminderNoteAsync(string id);
        Task DeleteTodoNoteAsync(string id);
        Task DeleteBookmarkNoteAsync(string id);

        // Get by Id
        Task<RegularNoteDto> GetRegularNoteByIdAsync(string id);
        Task<ReminderNoteDto> GetReminderNoteByIdAsync(string id);
        Task<TodoNoteDto> GetTodoNoteByIdAsync(string id);
        Task<BookmarkNoteDto> GetBookmarkNoteByIdAsync(string id);

        // Get All Async(User-specific)
        Task<IEnumerable<RegularNoteDto>> GetAllRegularNotesAsync();
        Task<IEnumerable<ReminderNoteDto>> GetAllReminderNotesAsync();
        Task<IEnumerable<TodoNoteDto>> GetAllTodoNotesAsync();
        Task<IEnumerable<BookmarkNoteDto>> GetAllBookmarkNotesAsync();
    }
}
