using Authentication.Client.ClientAuthentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;



    namespace Authentication.ServerAuthentication
    {
        public class CustomServerAuthenticationProvider : ServerAuthenticationStateProvider, IDisposable
        {
            private readonly PersistentComponentState _componentState;
            private Task<AuthenticationState>? _authenticationState;
            private readonly PersistingComponentStateSubscription _subscription;

            public CustomServerAuthenticationProvider(PersistentComponentState persistentComponentState)
            {
                _componentState = persistentComponentState;
                AuthenticationStateChanged += CustomServerAuthenticationProvider_AuthenticationStateChanged;
                _subscription = persistentComponentState.RegisterOnPersisting(PersistentComponentStateAsync, RenderMode.InteractiveWebAssembly);
            }

            private async Task PersistentComponentStateAsync()
            {
                var authState = await _authenticationState!;
                if (authState.User.Identity!.IsAuthenticated)
                {
                    var claims = authState.User.Claims.ToList();
                    var role = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)?.Value;
                    var username = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)?.Value;
                    var id = claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;

                    // Ensure valid claims exist
                    if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(id))
                    {
                        var customClaim = new CustomClaim(role, username, id);
                        _componentState.PersistAsJson(nameof(CustomClaim), customClaim);
                    }
                }
            }

            private void CustomServerAuthenticationProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
            {
                _authenticationState = task;
            }

            // This method extracts and decodes the JWT token, then updates the authentication state.
            public async Task SetAuthenticationStateFromJwt(string jwtToken)
            {
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    var jwtHandler = new JwtSecurityTokenHandler();

                    if (jwtHandler.CanReadToken(jwtToken))
                    {
                        var jwtTokenObj = jwtHandler.ReadJwtToken(jwtToken);

                        // Extract claims from the JWT token
                        var claims = jwtTokenObj.Claims.ToList();

                        // Create the ClaimsPrincipal
                        var identity = new ClaimsIdentity(claims, "jwt");
                        var claimsPrincipal = new ClaimsPrincipal(identity);

                        // Notify the authentication state change
                        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

                        // Persist the claims
                        var role = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)?.Value;
                        var username = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)?.Value;
                        var id = claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;

                        if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(id))
                        {
                            var customClaim = new CustomClaim(role, username, id);
                            _componentState.PersistAsJson(nameof(CustomClaim), customClaim);
                        }
                    }
                    else
                    {
                        // Invalid token, create an anonymous user
                        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
                    }
                }
                else
                {
                    // No token, create an anonymous user
                    var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
                }
            }

            public void MarkUserAsLoggedOut()
            {
                // Create a new ClaimsPrincipal with no identity
                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

                // Update the authentication state
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
            }

            public void Dispose()
            {
                _subscription.Dispose();
                AuthenticationStateChanged -= CustomServerAuthenticationProvider_AuthenticationStateChanged;
            }
        }
    }

    //public class CustomServerAuthenticationProvider : ServerAuthenticationStateProvider, IDisposable
    //{
    //    private readonly PersistentComponentState _componentState;
    //    private Task<AuthenticationState>? _authenticationState;
    //    private readonly PersistingComponentStateSubscription _subscription;

    //    public CustomServerAuthenticationProvider(PersistentComponentState persistentComponentState)
    //    {
    //        _componentState = persistentComponentState;
    //        AuthenticationStateChanged += CustomServerAuthenticationProvider_AuthenticationStateChanged;
    //        _subscription = persistentComponentState.RegisterOnPersisting(PersistentComponentStateAsync, RenderMode.InteractiveWebAssembly);
    //    }

    //    private async Task PersistentComponentStateAsync()
    //    {
    //        var authState = await _authenticationState!;
    //        if (authState.User.Identity!.IsAuthenticated)
    //        {
    //            var claims = authState.User.Claims;
    //            var role = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)!.Value;
    //            var username = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)!.Value;
    //            var id = claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)!.Value;
    //            var customClaim = new CustomClaim(role, username, id);
    //            _componentState.PersistAsJson(nameof(CustomClaim), customClaim);
    //        }
    //    }

    //    private void CustomServerAuthenticationProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
    //    {
    //        _authenticationState = task;
    //    }

    //    public void MarkUserAsLoggedOut()
    //    {
    //        // Create a new ClaimsPrincipal with no identity
    //        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

    //        // Update the authentication state
    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    //    }

    //    public void Dispose()
    //    {
    //        _subscription.Dispose();
    //        AuthenticationStateChanged -= CustomServerAuthenticationProvider_AuthenticationStateChanged;
    //    }
    //}
//}


