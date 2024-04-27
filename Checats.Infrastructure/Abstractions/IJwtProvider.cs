using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Models;

namespace Checats.Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(UserEntity userEntity);
}