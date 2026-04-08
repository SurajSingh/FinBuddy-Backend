using FinCoach.Application.Onboarding;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinCoach.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OnboardingController : ControllerBase
{
    private readonly IMediator _mediator;

    public OnboardingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("complete")]
    public async Task<IActionResult> CompleteOnboarding([FromBody] CompleteOnboardingRequest request)
    {
        // For now, get UserId from request or use a default for testing
        // Ideally this comes from the JWT token: User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) && request.UserId == Guid.Empty)
        {
            return Unauthorized();
        }

        var userId = !string.IsNullOrEmpty(userIdString) ? Guid.Parse(userIdString) : request.UserId;

        var command = new CompleteOnboardingCommand(
            userId,
            request.MonthlyIncome,
            request.PrimaryCurrency,
            request.RiskAppetite,
            request.Expenses,
            request.EMIs,
            request.Goals);

        await _mediator.Send(command);
        return Ok(new { message = "Onboarding completed successfully" });
    }
}

public class CompleteOnboardingRequest
{
    public Guid UserId { get; set; }
    public decimal MonthlyIncome { get; set; }
    public string PrimaryCurrency { get; set; } = "AED";
    public FinCoach.Domain.Enums.RiskAppetite RiskAppetite { get; set; }
    public List<InitialExpenseDto> Expenses { get; set; } = new();
    public List<InitialEMIDto> EMIs { get; set; } = new();
    public List<InitialGoalDto> Goals { get; set; } = new();
}
