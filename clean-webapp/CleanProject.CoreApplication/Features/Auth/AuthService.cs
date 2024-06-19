using CleanProject.CoreApplication.Infrastructure.Token;

namespace CleanProject.CoreApplication.Features.Auth;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IAuthRepository authRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
    }

    public async Task<CreateUserResult> HandleAsync(CreateUserCommand command)
    {
        var result = await _authRepository.CreateUserAsync(command.Email, command.Email, command.Password);

        if (result == null)
        {
            throw new Exception("Could not create user");
        }
        
        var token = _tokenService.GenerateJwtTokenForClaims(result);
        return new CreateUserResult(token);
    }
    public async Task<GetUserResponse> Handle(GetUserQuery query)
    {
        var user = await _authRepository.GetUserAsync(query.Email, query.Password);
        if (user == null) throw new Exception("could not find user");
        var token = _tokenService.GenerateJwtTokenForClaims(user);
        return new GetUserResponse(token);
    }
}