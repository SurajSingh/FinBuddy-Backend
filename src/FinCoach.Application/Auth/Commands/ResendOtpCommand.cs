using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record ResendOtpCommand(string Email) : IRequest<bool>;

public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, bool>
{
    public Task<bool> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
    {
        // Mock resend logic
        return Task.FromResult(true);
    }
}
