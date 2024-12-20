using NotesManagement.Api.DTOs.Notes;
using NotesManagement.Api.DTOs;
using NotesManagement.Api.Entities.Notes;
using NotesManagement.Api.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace NotesManagement.Api.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            // AuthService Mappings
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();

            // Notes Mappings
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<RegularNote, RegularNoteDto>().ReverseMap();
            CreateMap<ReminderNote, ReminderNoteDto>().ReverseMap();
            CreateMap<TodoNote, TodoNoteDto>().ReverseMap();
            CreateMap<BookmarkNote, BookmarkNoteDto>().ReverseMap();
        }
    }
}
