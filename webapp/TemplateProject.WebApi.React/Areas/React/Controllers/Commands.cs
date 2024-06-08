namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public record CreateBookCommand(string Title, string Author, int Year);

public record UpdateBookCommand(Guid Id, string Title, string Author, int Year);