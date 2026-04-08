using FinCoach.Application.Interfaces;
using FinCoach.Domain.Entities;
using MediatR;

namespace FinCoach.Application.Auth.Commands;

public record RegisterUserCommand(string Email, string Name, string PhoneNumber) : IRequest<Guid>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingEmail = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (existingEmail != null)
        {
            throw new Exception("Strategic Error: This email identity is already synchronized.");
        }

        var existingPhone = await _unitOfWork.Users.GetByPhoneNumberAsync(request.PhoneNumber);
        if (existingPhone != null)
        {
            throw new Exception("Strategic Error: This mobile line is already linked to an existing architect.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            DisplayName = request.Name,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
