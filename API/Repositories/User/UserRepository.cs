using API.DataContext;
using Microsoft.EntityFrameworkCore;
using Shared.Contract.User;
using Shared.Models;

namespace API.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(UserEntity user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task<UserEntity?> GetUserByUsernameAsync(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return null;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
