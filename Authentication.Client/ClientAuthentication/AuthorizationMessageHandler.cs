using Authentication.Client.Common;
using Microsoft.AspNetCore.Components.Authorization;

namespace Authentication.Client.ClientAuthentication
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthorizationMessageHandler(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                string jwtToken = authState.ExtractJWTFromToken();
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
