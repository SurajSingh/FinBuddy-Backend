using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record VerifyOtpCommand(string Email, string Code) : IRequest<bool>;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, bool>
{
    public Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        // Mock verification: 123456 is always valid
        if (request.Code == "123456")
        {
            return Task.FromResult(true);
        }
        
        return Task.FromResult(false);
    }
}
