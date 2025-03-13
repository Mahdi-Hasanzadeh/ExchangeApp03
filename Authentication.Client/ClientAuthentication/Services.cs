using Authentication.Client.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace Authentication.Client.ClientAuthentication
{
    public class Services
    {
        
        /// <summary>
        /// Set the culture for the front and server section of webassembly project.
        /// </summary>
        /// <param name="host">An instance of WebassemblyHost</param>
        /// <returns>No Return</returns>
        public static async Task SetCulture(WebAssemblyHost host)
        {
            const string cultureNameInLocalStorage = "culture";
            const string defaultCulture = "en-US";
            var js = host.Services.GetRequiredService<IJSRuntime>();

            try
            {
                //var httpClient = host.Services.GetRequiredService<HttpClient>();
                var Navigation = host.Services.GetRequiredService<NavigationManager>();

                var http = HttpClientManager.GetBlazorServerClient();
                var cultureFromCookie = await http.GetStringAsync("api/Cookie/GetCulture");

                var SavedCultureInLocalStorage = await js.InvokeAsync<string>("localStorage.getItem", cultureNameInLocalStorage);

                if (SavedCultureInLocalStorage == null)
                {
                    await js.InvokeVoidAsync("localStorage.setItem", cultureNameInLocalStorage, defaultCulture);
                }

                CultureInfo culture = CultureInfo.GetCultureInfo(SavedCultureInLocalStorage ?? defaultCulture);

                if (cultureFromCookie != SavedCultureInLocalStorage)
                {
                    Helper.SetCultureInCookie(Navigation, culture);
                }

                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                await js.InvokeVoidAsync("localStorage.setItem", cultureNameInLocalStorage, defaultCulture);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
