using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Client.ClientAuthentication
{
    public class ClientAuthenticationStateProvider : AuthenticationStateProvider,IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        //private readonly Timer _tokenCheckTimer;
        private PeriodicTimer _tokenCheckTimer;
        private CancellationTokenSource _cts = new();
        public ClientAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            // ✅ Set up a timer to check token validity every 10 seconds
            //_tokenCheckTimer = new Timer(async _ => await CheckTokenValidity(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            _tokenCheckTimer = new PeriodicTimer(TimeSpan.FromMinutes(10)); // ⏳ Check every 10min
            Task.Run(async () =>
            {
                while (await _tokenCheckTimer.WaitForNextTickAsync(_cts.Token))
                {
                    await CheckTokenValidity();
                }
            }, _cts.Token);
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _tokenCheckTimer?.Dispose();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");

            if (string.IsNullOrWhiteSpace(token) || !IsValidJwt(token))
            {
                await SignOutAsync(); // Ensure logout if token is invalid
                return new AuthenticationState(_currentUser);
            }

            var user = ParseJwt(token);
            _currentUser = new ClaimsPrincipal(user);
            return new AuthenticationState(_currentUser);
        }

        public async Task SignInAsync(string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken) || !IsValidJwt(jwtToken))
            {
                throw new ArgumentException("Invalid JWT token.");
            }

            // ✅ Store JWT in localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt_token", jwtToken);

            // ✅ Extract claims from JWT
            var user = ParseJwt(jwtToken);
            _currentUser = new ClaimsPrincipal(user);

            // ✅ Notify Blazor about the authentication state change
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));


        }

        public async Task SignOutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwt_token");

            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }


        private ClaimsIdentity ParseJwt(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                if (!IsValidJwt(jwtToken) && !handler.CanReadToken(jwtToken))
                {
                    return new ClaimsIdentity();
                }

                var token = handler.ReadJwtToken(jwtToken);
                var claims = token.Claims.ToList();

                // Extract necessary claims from the token (Modify based on your token structure)
                var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "UnknownUser";
                var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User";
                var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";

                // ✅ Add custom claims
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

                // ✅ Store the JWT token itself as a claim for easy retrieval
                claims.Add(new Claim("token", jwtToken));

                return new ClaimsIdentity(claims, "jwt");
            }
            catch (Exception ex)
            {
                return new ClaimsIdentity();

            }

        }

        private async Task CheckTokenValidity()
        {
            try
            {

                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt_token");

                if (string.IsNullOrWhiteSpace(token) || !IsValidJwt(token))
                {
                    await SignOutAsync();
                    return;
                }

                //var user = ParseJwt(token);
                //_currentUser = new ClaimsPrincipal(user);
                //NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            }
            catch (Exception ex)
            {

            }
        }


        private bool IsValidJwt(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token)) return false;

                var jwtToken = handler.ReadJwtToken(token);
                var expiryClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

                if (expiryClaim != null && long.TryParse(expiryClaim, out var expSeconds))
                {
                    var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                    return expiryDate > DateTimeOffset.UtcNow;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
