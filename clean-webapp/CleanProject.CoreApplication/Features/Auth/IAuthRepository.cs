using CleanProject.Domain;

namespace CleanProject.CoreApplication.Features.Auth;

public interface IAuthRepository
{
    Task<AppUser?> CreateUserAsync(string username, string email, string password);
    Task<AppUser?> GetUserAsync(string email, string password);
}