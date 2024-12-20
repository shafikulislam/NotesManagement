using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesManagement.Api.DTOs.Notes;
using NotesManagement.Api.Entities.Notes;
using NotesManagement.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesManagement.Api.Controllers
{
    [Route("api/notes")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        #region Regular Notes

        // POST: api/notes/regular
        [HttpPost("regular")]
        public async Task<IActionResult> CreateRegularNoteAsync([FromBody] RegularNoteDto note)
        {
            note = await _noteService.CreateRegularNoteAsync(note);
            return CreatedAtRoute("GetRegularNoteById", new { id = note.Id }, note);
        }

        // PUT: api/notes/regular/{id}
        [HttpPut("regular/{id}")]
        public async Task<IActionResult> UpdateRegularNoteAsync(string id, [FromBody] RegularNoteDto note)
        {
            if (id != note.Id)
                return BadRequest();

            note = await _noteService.UpdateRegularNoteAsync(note);
            return NoContent();
        }

        // DELETE: api/notes/regular/{id}
        [HttpDelete("regular/{id}")]
        public async Task<IActionResult> DeleteRegularNoteAsync(string id)
        {
            await _noteService.DeleteRegularNoteAsync(id);
            return NoContent();
        }

        // GET: api/notes/regular/{id}
        [HttpGet("regular/{id}", Name = "GetRegularNoteById")]
        public async Task<ActionResult<RegularNoteDto>> GetRegularNoteByIdAsync(string id)
        {
            var note = await _noteService.GetRegularNoteByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // GET: api/notes/regular
        [HttpGet("regular")]
        public async Task<ActionResult<IEnumerable<RegularNoteDto>>> GetAllRegularNotesAsync()
        {
            var notes = await _noteService.GetAllRegularNotesAsync();
            return Ok(notes);
        }

        #endregion

        #region Reminder Notes

        // POST: api/notes/reminder
        [HttpPost("reminder")]
        public async Task<IActionResult> CreateReminderNoteAsync([FromBody] ReminderNoteDto note)
        {
            note = await _noteService.CreateReminderNoteAsync(note);

            return CreatedAtRoute("GetReminderNoteById", new { id = note.Id }, note);
        }

        // PUT: api/notes/reminder/{id}
        [HttpPut("reminder/{id}")]
        public async Task<IActionResult> UpdateReminderNoteAsync(string id, [FromBody] ReminderNoteDto note)
        {
            if (id != note.Id)
                return BadRequest();

            note = await _noteService.UpdateReminderNoteAsync(note);
            return NoContent();
        }

        // DELETE: api/notes/reminder/{id}
        [HttpDelete("reminder/{id}")]
        public async Task<IActionResult> DeleteReminderNoteAsync(string id)
        {
            await _noteService.DeleteReminderNoteAsync(id);
            return NoContent();
        }

        // GET: api/notes/reminder/{id}
        [HttpGet("reminder/{id}", Name = "GetReminderNoteById")]
        public async Task<ActionResult<ReminderNoteDto>> GetReminderNoteByIdAsync(string id)
        {
            var note = await _noteService.GetReminderNoteByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // GET: api/notes/reminder
        [HttpGet("reminder")]
        public async Task<ActionResult<IEnumerable<ReminderNoteDto>>> GetAllReminderNotesAsync()
        {
            var notes = await _noteService.GetAllReminderNotesAsync();
            return Ok(notes);
        }

        #endregion

        #region Todo Notes

        // POST: api/notes/todo
        [HttpPost("todo")]
        public async Task<IActionResult> CreateTodoNoteAsync([FromBody] TodoNoteDto note)
        {
            note = await _noteService.CreateTodoNoteAsync(note);
            return CreatedAtRoute("GetTodoNoteById", new { id = note.Id }, note);
        }

        // PUT: api/notes/todo/{id}
        [HttpPut("todo/{id}")]
        public async Task<IActionResult> UpdateTodoNoteAsync(string id, [FromBody] TodoNoteDto note)
        {
            if (id != note.Id)
                return BadRequest();

            note = await _noteService.UpdateTodoNoteAsync(note);
            return NoContent();
        }

        // DELETE: api/notes/todo/{id}
        [HttpDelete("todo/{id}")]
        public async Task<IActionResult> DeleteTodoNoteAsync(string id)
        {
            await _noteService.DeleteTodoNoteAsync(id);
            return NoContent();
        }

        // GET: api/notes/todo/{id}
        [HttpGet("todo/{id}", Name = "GetTodoNoteById")]
        public async Task<ActionResult<TodoNoteDto>> GetTodoNoteByIdAsync(string id)
        {
            var note = await _noteService.GetTodoNoteByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // GET: api/notes/todo
        [HttpGet("todo")]
        public async Task<ActionResult<IEnumerable<TodoNoteDto>>> GetAllTodoNotesAsync()
        {
            var notes = await _noteService.GetAllTodoNotesAsync();
            return Ok(notes);
        }

        #endregion

        #region Bookmark Notes

        // POST: api/notes/bookmark
        [HttpPost("bookmark")]
        public async Task<IActionResult> CreateBookmarkNoteAsync([FromBody] BookmarkNoteDto note)
        {
            note = await _noteService.CreateBookmarkNoteAsync(note);
            return CreatedAtRoute("GetBookmarkNoteById", new { id = note.Id }, note);
        }

        // PUT: api/notes/bookmark/{id}
        [HttpPut("bookmark/{id}")]
        public async Task<IActionResult> UpdateBookmarkNoteAsync(string id, [FromBody] BookmarkNoteDto note)
        {
            if (id != note.Id)
                return BadRequest();

            note = await _noteService.UpdateBookmarkNoteAsync(note);
            return NoContent();
        }

        // DELETE: api/notes/bookmark/{id}
        [HttpDelete("bookmark/{id}")]
        public async Task<IActionResult> DeleteBookmarkNoteAsync(string id)
        {
            await _noteService.DeleteBookmarkNoteAsync(id);
            return NoContent();
        }

        // GET: api/notes/bookmark/{id}
        [HttpGet("bookmark/{id}", Name = "GetBookmarkNoteById")]
        public async Task<ActionResult<BookmarkNoteDto>> GetBookmarkNoteByIdAsync(string id)
        {
            var note = await _noteService.GetBookmarkNoteByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // GET: api/notes/bookmark
        [HttpGet("bookmark")]
        public async Task<ActionResult<IEnumerable<BookmarkNoteDto>>> GetAllBookmarkNotesAsync()
        {
            var notes = await _noteService.GetAllBookmarkNotesAsync();
            return Ok(notes);
        }

        #endregion
    }
}
