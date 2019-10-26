using System.Security.Claims;
using System.Security.Principal;

namespace Auth.JWT
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public IIdentity Identity { get; set; }
    }
}
