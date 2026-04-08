using FinCoach.Application.Interfaces;
using FinCoach.Domain.Enums;
using MediatR;

namespace FinCoach.Application.Transactions.Queries;

public record TransactionDto(
    Guid Id,
    decimal Amount,
    string Currency,
    string Category,
    string Notes,
    DateTime Date,
    int Type
);

public record GetRecentTransactionsQuery(Guid UserId, int Count = 5) : IRequest<IEnumerable<TransactionDto>>;

public class GetRecentTransactionsQueryHandler : IRequestHandler<GetRecentTransactionsQuery, IEnumerable<TransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRecentTransactionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _unitOfWork.Transactions.GetByUserIdAsync(request.UserId);
        
        return transactions
            .OrderByDescending(t => t.Date)
            .Take(request.Count)
            .Select(t => new TransactionDto(
                t.Id,
                t.Amount,
                t.Currency,
                t.Category,
                t.Notes,
                t.Date,
                (int)t.Type
            ));
    }
}
