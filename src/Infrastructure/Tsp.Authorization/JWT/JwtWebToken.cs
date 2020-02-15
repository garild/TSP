using System.Collections.Generic;
using System.Security.Principal;

namespace Tsp.Authorization.JWT
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public IIdentity Identity { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
