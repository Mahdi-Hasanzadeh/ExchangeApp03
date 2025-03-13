using Shared;

namespace Authentication.Client.ClientAuthentication
{
    public class HttpClientManager
    {
        private readonly IServiceProvider _serviceProvider;

        public HttpClientManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static HttpClient GetApiClient()
        {
            return new HttpClient { BaseAddress = new Uri(Utility.API_URI) };
        }

        public static HttpClient GetBlazorServerClient()
        {
            return new HttpClient { BaseAddress = new Uri(Utility.BlazorServer_URI) };
        }
    }

}
