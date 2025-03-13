using Authentication.Client.Common;
using Authentication.Client.Repository.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Shared;
using Shared.DTOs;
using Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Authentication.Client.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<UserEntity>> GetUserByIdAsync(int id,string jwtToken)
        {
            
            ApiResponse<UserEntity> response = null;
            try
            {
                // Construct the URL
                var url = $"api/User/{id}";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.
                AuthenticationHeaderValue("Bearer", jwtToken);
                // Send the GET request
                response = await _httpClient.GetFromJsonAsync<ApiResponse<UserEntity>>(url);

                return response;


            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Handle 401 Unauthorized
                Console.WriteLine("Unauthorized access.");
                return new ApiResponse<UserEntity>(false, "You are not authorized", response.ErrorCode);

            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return new ApiResponse<UserEntity>(false, "error", response.ErrorCode);
            }
        }
    }
}
