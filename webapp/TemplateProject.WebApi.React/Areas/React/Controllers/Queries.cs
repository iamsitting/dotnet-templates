namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public record GetBookByIdQuery(Guid Id);
public record GetAllBooksQuery(bool ExcludeArchive = false);