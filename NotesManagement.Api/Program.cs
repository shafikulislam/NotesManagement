using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotesManagement.Api.Mappings;
using NotesManagement.Api.Services.Interfaces;
using NotesManagement.Api.Services.Implementations;
using NotesManagement.Api.Entities;
using NotesManagement.Api.Repositories.Implementations;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Entities.Notes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped(typeof(IIOService<>), typeof(IOService<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IIOService<User>, IOService<User>>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//builder.Services.AddScoped<IBaseRepository<RegularNote>, BaseRepository<RegularNote>>();
//builder.Services.AddScoped<IIOService<RegularNote>, IOService<RegularNote>>();
//builder.Services.AddScoped<IBaseRepository<ReminderNote>, BaseRepository<ReminderNote>>();
//builder.Services.AddScoped<IIOService<ReminderNote>, IOService<ReminderNote>>();
//builder.Services.AddScoped<IBaseRepository<TodoNote>, BaseRepository<TodoNote>>();
//builder.Services.AddScoped<IIOService<TodoNote>, IOService<TodoNote>>();
//builder.Services.AddScoped<IBaseRepository<BookmarkNote>, BaseRepository<BookmarkNote>>();
//builder.Services.AddScoped<IIOService<BookmarkNote>, IOService<BookmarkNote>>();
builder.Services.AddScoped<INoteService, NoteService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("N56%$$32927hhdg6DGS&*2kkkdd@56Hjh$$32927hhdg6DGS&*2kkkdd00"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.MapControllers();

app.Run();
