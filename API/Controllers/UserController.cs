using API.DataContext;
using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Contract;
using Shared.Contract.User;
using Shared.DTOs;
using Shared.Models;
using Shared.Roles;
using Shared.View_Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGenericRepository<UserEntity> _genericRepository;
        private readonly IUserRepository _userRepo;

        public UserController(ILogger<UserController> logger,
            IGenericRepository<UserEntity> genericRepository,
            IUserRepository userRepo)
        {
            _logger = logger;
            _genericRepository = genericRepository;
            _userRepo = userRepo;
        }

        [HttpGet("{id:int}", Name = "GetUserById")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserEntity>>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepo.GetUserByIdAsync(id);

                if (user == null)
                {
                    _logger.LogInformation("user not found");
                    return NotFound(new ApiResponse<UserEntity>(false, "User not found", nameof(ResourceStringsError.UserNotFound)));
                }

                return Ok(new ApiResponse<UserEntity>(true, "User found", null, user));
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, "Something gone wrong", nameof(ResourceStringsError.ServerError)));
            }

        }

        [HttpPost("register", Name = "RegisterUser")]
        public async Task<ActionResult<ApiResponse<UserEntity>>> RegisterUser(RegisterViewModel newUser)
        {
            try
            {
                await _userRepo.BeginTransaction();
                _logger.LogInformation("Regitser User in invoked");
                var user = await _userRepo.GetUserByUsernameAsync(newUser.Username);
                if (user != null)
                {
                    return Ok(new ApiResponse<UserEntity>(false, "Username already exist", nameof(ResourceStringsError.UsernameAlreadyExist)));
                }

                
                //if (!string.IsNullOrEmpty(newUser.Email))
                //{
                //    user = await _userRepo.GetUserByEmailAsync(newUser.Email);
                //    if (user != null)
                //    {
                //        return Ok(new ApiResponse<UserEntity>(false, "Email already exist", nameof(ResourceStringsError.EmailAlreadyExist)));
                //    }
                //}



                string hashedPassword = newUser.Password.HashPassword();

                // Connection string using the provided server and admin credentials
                var connectionString = $"Server={newUser.ServerName};User Id={newUser.Username};Password={newUser.Password};Trusted_Connection=true;TrustServerCertificate=true";

                // Create a new database for the user
                var dbName = $"ExchangeDB_{newUser.Username}";
                //using (var connection = new SqlConnection(connectionString))
                //{
                //    await connection.OpenAsync();

                    
                //    var createDbCommand = new SqlCommand($"CREATE DATABASE {dbName}", connection);

                //    await createDbCommand.ExecuteNonQueryAsync();
                //}

                // Now, change the connection string to point to the newly created database
                var userDbConnectionString = $"Server={newUser.ServerName};Database={dbName};User Id={newUser.Username};Password={newUser.Password};Trusted_Connection=true;TrustServerCertificate=true";

                // Use the DbContext to apply migrations to the newly created database
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(userDbConnectionString);
                UserDatabaseService userDatabaseService = new UserDatabaseService();
                userDatabaseService.SetConnectionString(userDbConnectionString);
                using (var dbContext = new AppDbContext(optionsBuilder.Options, userDatabaseService))
                {
                    //// Ensure the database exists (without recreating it)
                    //await dbContext.Database.EnsureCreatedAsync();

                    // Apply migrations (only if needed)
                    //await dbContext.Database.BeginTransactionAsync();
                    await dbContext.Database.MigrateAsync();
                    
                    // add data to database

                }

                // Change the Password to hashedPassword for security
                newUser.Password = hashedPassword;

                //newUser.Role = string.IsNullOrEmpty(newUser.Role) ? nameof(Roles.Guest) : newUser.Role;

                var userEntity = new UserEntity() { Username = newUser.Username,
                    Password = newUser.Password,
                    Role = Roles.Administrator.ToString(),
                    ServerAddress = newUser.ServerName,
                    Databasename = $"ExchangeDB_{newUser.Username}",
                    ConnectionString = userDbConnectionString,
                    CreatedDate = DateTime.Now,
                    ValidUntil = DateTime.Now.AddDays(1),
                };

                var addedUser = await _userRepo.AddUserAsync(userEntity);
                if (!addedUser)
                {
                    await _userRepo.RollbackTransaction();
                }

                await _userRepo.SaveAsync();
                await _userRepo.CommitTransaction();
                return Ok(new ApiResponse<UserEntity>(true, "User created successfully"));
            }
            catch (Exception ex)
            {
                await _userRepo.RollbackTransaction();
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<UserEntity>(false, ex.Message, nameof(ResourceStringsError.ServerError)));
            }
        }

        [HttpPost("login", Name = "LoginUser")]
        public async Task<ActionResult<ApiResponse<UserEntity>>> LoginUser([FromBody] LoginViewModel loginModel)
        {
            try
            {
                // check if loginModel is null(username or password)

                var user = await _userRepo.GetUserByUsernameAsync(loginModel.Username);
                if (user == null)
                {
                    return NotFound(new ApiResponse<UserEntity>(false, "User not found",
                        nameof(ResourceStringsError.UserNotFound)));
                }


                if (!loginModel.Password.VerifyPassword(user.Password))
                {
                    return BadRequest(new ApiResponse<UserEntity>(false, "Username or Password is wrong",
                        nameof(ResourceStringsError.InvalidCredentials)));
                }

                var claims = new List<Claim>
                     {
                        new Claim(ClaimTypes.Name,user?.Username!),
                        new Claim(ClaimTypes.Role,user?.Role!),
                        new Claim(ClaimTypes.NameIdentifier,user?.Id.ToString()!),
                        new Claim("ConnectionString", user?.ConnectionString!) // Store connection string in JWT
                     };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mahdihasanzadeh12313kljuwiorqyhfdjk"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                var jwtToken = new JwtSecurityToken(
                    issuer: "https://localhost:7210",
                    audience: "https://localhost:7210",
                    claims: claims,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: creds);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return Ok(new ApiResponse<string>(true, "Token created", null, token));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message, nameof(ResourceStringsError.ServerError)));
            }
        }

        [HttpPut("update", Name = "UpdateUser")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> UpdateUser([FromBody] UpdateInfoViewModel updateInfoModel)
        {
            try
            {
                var userInfo = await _userRepo.GetUserByIdAsync(updateInfoModel.Id);
                if (userInfo == null)
                {
                    return NotFound(new ApiResponse<UserEntity>(false, "User not found",
                        nameof(ResourceStringsError.UserNotFound)));
                }
                if (userInfo.Username == updateInfoModel.Username
                    && userInfo.Email == updateInfoModel.Email
                    && userInfo.PictureUrl == updateInfoModel.PictureUrl
                    )
                {
                    return Ok(new ApiResponse<UserEntity>(false, "Nothing Changed",
                                            nameof(ResourceStringsError.NoChangesToUserAccount)));
                }
                if (userInfo.Username != updateInfoModel.Username)
                {
                    var user = await _userRepo.GetUserByUsernameAsync(updateInfoModel.Username);
                    if (user != null)
                    {
                        return BadRequest(new ApiResponse<UserEntity>(false, "Username is not available",
                            nameof(ResourceStringsError.UsernameAlreadyExist)));
                    }
                }
                if (userInfo.Email != updateInfoModel.Email)
                {
                    var user = await _userRepo.GetUserByEmailAsync(updateInfoModel.Email);
                    if (user != null)
                    {
                        return BadRequest(new ApiResponse<UserEntity>(false, "Email address is not available",
                            nameof(ResourceStringsError.EmailAlreadyExist)));
                    }
                }

                if (!updateInfoModel.Password.VerifyPassword(userInfo.Password))
                {
                    return BadRequest(new ApiResponse<UserEntity>(false, "Username or Password is wrong",
                        nameof(ResourceStringsError.InvalidCredentials)));
                }

                userInfo.Username = updateInfoModel.Username;
                userInfo.Email = updateInfoModel.Email;
                userInfo.PictureUrl = updateInfoModel.PictureUrl;

                await _userRepo.SaveAsync();

                return Ok(new ApiResponse<UserEntity>(true, "User account updated", nameof(ResourceStringsSuccessMessage.AccountUpdated)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message, nameof(ResourceStringsError.ServerError)));
            }
        }

        private static List<ClaimDto> GenerateClaims(UserEntity? user)
        {
            var claims = new List<Claim>
                     {
                        new Claim(ClaimTypes.Name,user?.Username!),
                        new Claim(ClaimTypes.Role,user?.Role!),
                        new Claim(ClaimTypes.NameIdentifier,user?.Id.ToString()!)
                     };
            var claimDtos = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList();
            return claimDtos;
        }
    }
}
