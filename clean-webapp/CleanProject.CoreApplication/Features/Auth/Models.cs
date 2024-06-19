namespace CleanProject.CoreApplication.Features.Auth;

public record CreateUserCommand(string Email, string Password);

public record CreateUserResult(string Token);
public record GetUserQuery(string Email, string Password);

public record GetUserResponse(string Token);
