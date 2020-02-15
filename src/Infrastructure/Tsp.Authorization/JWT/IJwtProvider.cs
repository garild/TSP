
namespace Tsp.Authorization.JWT
{
    public interface IJwtProvider
    {
        JsonWebToken Create(JwtUserDto userDto, string[] userRole);
    }
}
