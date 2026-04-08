using FinCoach.Application.Interfaces;
using FinCoach.Domain.Entities;
using FinCoach.Domain.Enums;
using MediatR;

namespace FinCoach.Application.Goals.Commands;

public record CreateGoalCommand(
    Guid UserId,
    string Name,
    decimal TargetAmount,
    DateTime TargetDate
) : IRequest;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var goal = new Goal
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Name = request.Name,
            TargetAmount = request.TargetAmount,
            Currency = user.PrimaryCurrency,
            CurrentAmount = 0,
            TargetDate = request.TargetDate,
            Status = GoalStatus.OnTrack,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Goals.AddAsync(goal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
