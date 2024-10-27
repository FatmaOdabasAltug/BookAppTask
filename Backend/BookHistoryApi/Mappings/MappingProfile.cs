using AutoMapper;
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create a mapping between BookHistory and BookHistoryDto
            CreateMap<BookHistory, BookHistoryDto>() // Entity to DTO
                .ReverseMap(); // DTO to Entity

            // Create a mapping between Book and BookDto
            CreateMap<Book, BookDto>() // Entity to DTO
                .ReverseMap(); // DTO to Entity
        }
    }
}