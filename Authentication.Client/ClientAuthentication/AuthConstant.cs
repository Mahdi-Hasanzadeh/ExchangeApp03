using Microsoft.AspNetCore.Authentication.Cookies;

namespace Authentication.Client.ClientAuthentication
{
    public class AuthConstant
    {
        public const string Scheme = CookieAuthenticationDefaults.AuthenticationScheme;
        public const string CookieName = "auth_token";
        public const int Time = 20;
    }
}
