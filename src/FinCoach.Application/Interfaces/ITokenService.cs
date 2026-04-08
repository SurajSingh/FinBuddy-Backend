using FinCoach.Domain.Entities;

namespace FinCoach.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
