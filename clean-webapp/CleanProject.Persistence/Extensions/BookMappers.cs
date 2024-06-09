using CleanProject.CoreApplication.Domain.Books;
using CleanProject.Persistence.EF.Entities;

namespace CleanProject.Persistence.Extensions;

public static class BookMappers
{
    public static BookDto AsDto(this Book entity)
    {
        return new BookDto()
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    public static Book MapToEntity(this UpdateBookDto dto, Book entity)
    {
        entity.Title = dto.Title;
        entity.Author = dto.Author;
        return entity;
    }
        
}