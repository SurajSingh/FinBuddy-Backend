using FinCoach.Application.Interfaces;
using FinCoach.Domain.Enums;
using MediatR;

namespace FinCoach.Application.Goals.Queries;

public record GoalDto(
    Guid Id,
    string Name,
    decimal TargetAmount,
    decimal CurrentAmount,
    string Currency,
    DateTime TargetDate,
    int Status
);

public record GetActiveGoalsQuery(Guid UserId) : IRequest<IEnumerable<GoalDto>>;

public class GetActiveGoalsQueryHandler : IRequestHandler<GetActiveGoalsQuery, IEnumerable<GoalDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetActiveGoalsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GoalDto>> Handle(GetActiveGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await _unitOfWork.Goals.GetByUserIdAsync(request.UserId);
        
        return goals
            .OrderBy(g => g.TargetDate)
            .Select(g => new GoalDto(
                g.Id,
                g.Name,
                g.TargetAmount,
                g.CurrentAmount,
                g.Currency,
                g.TargetDate,
                (int)g.Status
            ));
    }
}
