using FinCoach.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCoach.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(new { Id = response });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok(new { Message = "OTP Verified Successfully" });
            }
            return BadRequest(new { Message = "Invalid OTP" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ResendOtp([FromBody] ResendOtpCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(new { Message = "OTP Resent Successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("request-phone-otp")]
    public async Task<IActionResult> RequestPhoneOtp([FromBody] RequestOtpCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok(new { Message = "OTP Requested" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("verify-phone-otp")]
    public async Task<IActionResult> VerifyPhoneOtp([FromBody] VerifyPhoneOtpCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
