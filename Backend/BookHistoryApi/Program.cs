using BookHistoryApi.Data;
using BookHistoryApi.Services.Interfaces;
using BookHistoryApi.Services;
using Microsoft.EntityFrameworkCore;
using BookHistoryApi.Models.DTOs;
using BookHistoryApi.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Get the database connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register ApplicationDbContext and configure it to use SQL Server with the specified connection string


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controller services to the DI container
builder.Services.AddControllers();

// Register your AuthorService
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Register IBookService and BookService
builder.Services.AddScoped<IBookService, BookService>();

// Register IBookHistoryService and BookHistoryService
builder.Services.AddScoped<IBookHistoryService, BookHistoryService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();                
    app.UseSwaggerUI();              
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed dummy data at application startup
SeedDummyAuthors(app.Services);

app.Run();

void SeedDummyAuthors(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();
        
        // Dummy author data
        var dummyAuthors = new List<AuthorDto>
        {
            new AuthorDto { Id = 1, Name = "J.K. Rowling" },
            new AuthorDto { Id = 2, Name = "George R.R. Martin" },
            new AuthorDto { Id = 3, Name = "J.R.R. Tolkien" },
            new AuthorDto { Id = 4, Name = "Agatha Christie" },
            new AuthorDto { Id = 5, Name = "Mark Twain" },
            new AuthorDto { Id = 6, Name = "Ernest Hemingway" },
            new AuthorDto { Id = 7, Name = "Jane Austen" },
            new AuthorDto { Id = 8, Name = "F. Scott Fitzgerald" },
            new AuthorDto { Id = 9, Name = "Stephen King" },
            new AuthorDto { Id = 10, Name = "Leo Tolstoy" }
        };

        // Use the AuthorService to add the dummy authors
        foreach (var author in dummyAuthors)
        {
            authorService.AddAuthor(author);
        }
    }
}
