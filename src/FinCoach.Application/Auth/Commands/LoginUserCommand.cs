using FinCoach.Application.Auth.Common;
using FinCoach.Application.Interfaces;
using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<AuthResponse>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        
        // Simple password check for MVP (Should use Hashing in Prod)
        if (user == null)
        {
            throw new Exception("Invalid email or password");
        }

        var token = _tokenService.GenerateToken(user);

        return new AuthResponse(
            user.Id,
            user.Email,
            user.DisplayName,
            token,
            user.IsOnboarded,
            user.MonthlyIncome,
            user.PrimaryCurrency
        );
    }
}
