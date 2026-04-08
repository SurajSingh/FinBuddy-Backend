using FinCoach.Application.Auth.Common;
using FinCoach.Application.Interfaces;
using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record VerifyPhoneOtpCommand(string PhoneNumber, string Otp) : IRequest<AuthResponse>;

public class VerifyPhoneOtpCommandHandler : IRequestHandler<VerifyPhoneOtpCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public VerifyPhoneOtpCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(VerifyPhoneOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByPhoneNumberAsync(request.PhoneNumber);
        
        if (user == null || user.CurrentOtp != request.Otp)
        {
            throw new Exception("Invalid OTP");
        }

        // Clear OTP after successful verification
        user.CurrentOtp = null;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
