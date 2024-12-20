using AutoMapper;
using NotesManagement.Api.DTOs.Notes;
using NotesManagement.Api.Entities.Notes;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesManagement.Api.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IBaseRepository<RegularNote> _regularNoteRepo;
        private readonly IBaseRepository<ReminderNote> _reminderNoteRepo;
        private readonly IBaseRepository<TodoNote> _todoNoteRepo;
        private readonly IBaseRepository<BookmarkNote> _bookmarkNoteRepo;
        private readonly IMapper _mapper;

        public NoteService(
            IBaseRepository<RegularNote> regularNoteRepo,
            IBaseRepository<ReminderNote> reminderNoteRepo,
            IBaseRepository<TodoNote> todoNoteRepo,
            IBaseRepository<BookmarkNote> bookmarkNoteRepo,
            IMapper mapper)
        {
            _regularNoteRepo = regularNoteRepo;
            _reminderNoteRepo = reminderNoteRepo;
            _todoNoteRepo = todoNoteRepo;
            _bookmarkNoteRepo = bookmarkNoteRepo;
            _mapper = mapper;
        }

        #region Create

        public async Task<RegularNoteDto> CreateRegularNoteAsync(RegularNoteDto noteDto)
        {
            var note = _mapper.Map<RegularNote>(noteDto);
            note = await _regularNoteRepo.AddAsync(note);
            return _mapper.Map<RegularNoteDto>(note);
        }

        public async Task<ReminderNoteDto> CreateReminderNoteAsync(ReminderNoteDto noteDto)
        {
            var note = _mapper.Map<ReminderNote>(noteDto);
            note = await _reminderNoteRepo.AddAsync(note);
            return _mapper.Map<ReminderNoteDto>(note);
        }

        public async Task<TodoNoteDto> CreateTodoNoteAsync(TodoNoteDto noteDto)
        {
            var note = _mapper.Map<TodoNote>(noteDto);
            note = await _todoNoteRepo.AddAsync(note);
            return _mapper.Map<TodoNoteDto>(note);
        }

        public async Task<BookmarkNoteDto> CreateBookmarkNoteAsync(BookmarkNoteDto noteDto)
        {
            var note = _mapper.Map<BookmarkNote>(noteDto);
            note = await _bookmarkNoteRepo.AddAsync(note);
            return _mapper.Map<BookmarkNoteDto>(note);
        }

        #endregion

        #region Update

        public async Task<RegularNoteDto> UpdateRegularNoteAsync(RegularNoteDto noteDto)
        {
            var note = _mapper.Map<RegularNote>(noteDto);
            note = await _regularNoteRepo.UpdateAsync(note);
            return _mapper.Map<RegularNoteDto>(note);
        }

        public async Task<ReminderNoteDto> UpdateReminderNoteAsync(ReminderNoteDto noteDto)
        {
            var note = _mapper.Map<ReminderNote>(noteDto);
            note = await _reminderNoteRepo.UpdateAsync(note);
            return _mapper.Map<ReminderNoteDto>(note);
        }

        public async Task<TodoNoteDto> UpdateTodoNoteAsync(TodoNoteDto noteDto)
        {
            var note = _mapper.Map<TodoNote>(noteDto);
            note = await _todoNoteRepo.UpdateAsync(note);
            return _mapper.Map<TodoNoteDto>(note);
        }

        public async Task<BookmarkNoteDto> UpdateBookmarkNoteAsync(BookmarkNoteDto noteDto)
        {
            var note = _mapper.Map<BookmarkNote>(noteDto);
            note = await _bookmarkNoteRepo.UpdateAsync(note);
            return _mapper.Map<BookmarkNoteDto>(note);
        }

        #endregion

        #region Delete

        public async Task DeleteRegularNoteAsync(string id)
        {
            await _regularNoteRepo.DeleteAsync(id);
        }

        public async Task DeleteReminderNoteAsync(string id)
        {
            await _reminderNoteRepo.DeleteAsync(id);
        }

        public async Task DeleteTodoNoteAsync(string id)
        {
            await _todoNoteRepo.DeleteAsync(id);
        }

        public async Task DeleteBookmarkNoteAsync(string id)
        {
            await _bookmarkNoteRepo.DeleteAsync(id);
        }

        #endregion

        #region Get by Id

        public async Task<RegularNoteDto> GetRegularNoteByIdAsync(string id)
        {
            var note = await _regularNoteRepo.GetByIdAsync(id);
            return _mapper.Map<RegularNoteDto>(note);
        }

        public async Task<ReminderNoteDto> GetReminderNoteByIdAsync(string id)
        {
            var note = await _reminderNoteRepo.GetByIdAsync(id);
            return _mapper.Map<ReminderNoteDto>(note);
        }

        public async Task<TodoNoteDto> GetTodoNoteByIdAsync(string id)
        {
            var note = await _todoNoteRepo.GetByIdAsync(id);
            return _mapper.Map<TodoNoteDto>(note);
        }

        public async Task<BookmarkNoteDto> GetBookmarkNoteByIdAsync(string id)
        {
            var note = await _bookmarkNoteRepo.GetByIdAsync(id);
            return _mapper.Map<BookmarkNoteDto>(note);
        }

        #endregion

        #region Get All Async

        public async Task<IEnumerable<RegularNoteDto>> GetAllRegularNotesAsync()
        {
            var notes = await _regularNoteRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RegularNoteDto>>(notes);
        }

        public async Task<IEnumerable<ReminderNoteDto>> GetAllReminderNotesAsync()
        {
            var notes = await _reminderNoteRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReminderNoteDto>>(notes);
        }

        public async Task<IEnumerable<TodoNoteDto>> GetAllTodoNotesAsync()
        {
            var notes = await _todoNoteRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoNoteDto>>(notes);
        }

        public async Task<IEnumerable<BookmarkNoteDto>> GetAllBookmarkNotesAsync()
        {
            var notes = await _bookmarkNoteRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BookmarkNoteDto>>(notes);
        }

        #endregion
    }
}
