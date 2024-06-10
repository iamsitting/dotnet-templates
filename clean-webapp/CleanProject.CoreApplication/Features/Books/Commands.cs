using CleanProject.CoreApplication.Infrastructure;
using CleanProject.CoreApplication.Infrastructure.Template;
using CleanProject.Infrastructure.Templates;

namespace CleanProject.CoreApplication.Features.Books;

public record AddBookCommand(string Title, string Author, int Year);
public record UpdateBookCommand(Guid Id, string Title, string Author, int Year);

public class CommandHandler
{
    private readonly IBookRepository _repository;
    private readonly IAppLogger<CommandHandler> _logger;
    private readonly IEmailService _emailService;
    private readonly ITemplateService _templateService;

    public CommandHandler(IBookRepository repository, IAppLogger<CommandHandler> logger, IEmailService emailService, ITemplateService templateService)
    {
        _repository = repository;
        _logger = logger;
        _emailService = emailService;
        _templateService = templateService;
    }

    public async Task HandleAsync(AddBookCommand command)
    {
        if (command.Year > DateTime.Now.Year)
        {
            var ex = new Exception("Invalid");
            _logger.LogError("Failed to add book", ex);
            throw ex;
        }

        if (string.IsNullOrEmpty(command.Title) || string.IsNullOrEmpty(command.Author))
        {
            var ex = new Exception("Invalid");
            _logger.LogError("Failed to add book", ex);
            throw ex;
        }
        

        _repository.Add(new BookDto(command));
        
        var msg = await _templateService.GetTemplateFromParametersAsync(new ResetPasswordParameters("", "", "", ""));
        await _emailService.SendEmailGenericAsync("", "", "", msg);
        _logger.LogInformation("Success!");
    }
}