using DemoLogin.Domain.Models;

namespace DemoLogin.Api.Security
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
