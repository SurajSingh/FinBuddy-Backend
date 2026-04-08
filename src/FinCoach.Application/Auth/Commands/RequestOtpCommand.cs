using FinCoach.Application.Interfaces;
using FinCoach.Domain.Entities;
using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record RequestOtpCommand(string PhoneNumber) : IRequest<bool>;

public class RequestOtpCommandHandler : IRequestHandler<RequestOtpCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestOtpCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByPhoneNumberAsync(request.PhoneNumber);
        
        if (user == null)
        {
            throw new Exception("This mobile number is not registered. Please sign up first.");
        }

        // Set static OTP for MVP
        user.CurrentOtp = "123456";
        
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // In the future, this is where the SMS API integration would go
        // await _smsService.SendAsync(user.PhoneNumber, "Your FinBuddy OTP is 123456");

        return true;
    }
}
