using FinCoach.Application.Interfaces;
using MediatR;

namespace FinCoach.Application.Goals.Commands;

public record DeleteGoalCommand(Guid GoalId, Guid UserId) : IRequest;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _unitOfWork.Goals.GetByIdAsync(request.GoalId);
        
        if (goal == null || goal.UserId != request.UserId)
        {
            throw new Exception("Goal not found or access denied");
        }

        await _unitOfWork.Goals.DeleteAsync(goal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
