using Shared;
using Shared.DTOs;
using Shared.Models;


namespace Authentication.Client.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<ApiResponse<UserEntity>> GetUserByIdAsync(int id, string jwtToken);

    }

}
