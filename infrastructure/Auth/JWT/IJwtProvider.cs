
namespace Auth.JWT
{
    public interface IJwtProvider
    {
        JsonWebToken Create(JwtUserDto userDto, string[] userRole);
    }
}
