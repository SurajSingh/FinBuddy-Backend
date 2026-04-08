using System.Security.Claims;
using FinCoach.Application.Users.Queries;
using FinCoach.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCoach.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdClaim.Value);
        var query = new GetCurrentUserQuery(userId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);
        var command = new UpdateUserProfileCommand(
            userId,
            request.MonthlyIncome,
            request.PrimaryCurrency,
            request.RiskAppetite
        );

        await _mediator.Send(command);
        return Ok(new { message = "Profile updated successfully" });
    }

    [HttpPost("reset")]
    public async Task<IActionResult> ResetAccount()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);
        var command = new ResetUserCommand(userId);

        await _mediator.Send(command);
        return Ok(new { message = "Account reset successfully" });
    }
}

public class UpdateUserProfileRequest
{
    public decimal MonthlyIncome { get; set; }
    public string PrimaryCurrency { get; set; } = "AED";
    public int RiskAppetite { get; set; }
}
