using Authentication.ServerAuthentication;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.Roles;
using Shared.View_Model;
using System.Security.Claims;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly AppDbContext _context;

        //public UserController(AppDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<ApiResponse<UserEntity>>> GetUserById(int id)
        //{
        //    try
        //    {
        //        var user = await _context.Users.FindAsync(id);
        //        if (user == null)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "User not found"));
        //        }
        //        return Ok(new ApiResponse<UserEntity>(true, "User found", user));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message));
        //    }

        //}

        //[HttpPost("register")]
        //public async Task<ActionResult<ApiResponse<UserEntity>>> RegisterUser(UserEntity UserInfo)
        //{
        //    try
        //    {
        //        var user = await _context.Users.
        //            FirstOrDefaultAsync(u => u.Username == UserInfo.Username);
        //        if (user != null)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "Username already exist"));
        //        }

        //        //To-Do // check for the email existance

        //        //Hash the Password

        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(UserInfo.Password);
        //        UserEntity newUser = new UserEntity()
        //        {
        //            Username = UserInfo.Username,
        //            Email = UserInfo.Email,
        //            Password = hashedPassword,
        //            PictureUrl = UserInfo.PictureUrl,
        //            Role = UserInfo.Role ?? nameof(Roles.Guest)
        //        };
        //        var addedUser = await _context.Users.AddAsync(newUser);
        //        await _context.SaveChangesAsync();
        //        return Ok(new ApiResponse<UserEntity>(true, "User created successfully", addedUser.Entity));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message));
        //    }
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<ApiResponse<UserEntity>>> LoginUser([FromBody] LoginViewModel loginModel)
        //{
        //    try
        //    {
        //        var user = await _context.Users.
        //            FirstOrDefaultAsync(u => u.Username == loginModel.Username);
        //        if (user == null)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "Username not exist"));
        //        }
        //        //To-Do // check for the email existance

        //        //Hash the Password

        //        bool isPasswordMatched = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);
        //        if (!isPasswordMatched)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "Username or Password is wrong"));
        //        }

        //        var claims = new List<Claim>
        //             {
        //                new Claim(ClaimTypes.Name,user?.Username!),
        //                new Claim(ClaimTypes.Role,user?.Role!),
        //                new Claim(ClaimTypes.NameIdentifier,user?.Id.ToString()!)
        //             };
        //        var claimDtos = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList();

        //        return Ok(new ApiResponse<List<ClaimDto>>(true, "Claims Created", claimDtos));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message));
        //    }
        //}

        //[HttpPut("update")]
        //public async Task<ActionResult<ApiResponse<object>>> UpdateUser([FromBody] UpdateInfoViewModel updateInfoModel)
        //{
        //    try
        //    {
        //        var user = await _context.Users.
        //            FindAsync(updateInfoModel.Id);
        //        if (user == null)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "User not found"));
        //        }

        //        //Hash the Password

        //        bool isPasswordMatched = BCrypt.Net.BCrypt.Verify(updateInfoModel.Password, user.Password);
        //        if (!isPasswordMatched)
        //        {
        //            return Ok(new ApiResponse<UserEntity>(false, "Username or Password is wrong"));
        //        }

        //        user.Username = updateInfoModel.Username;
        //        user.Email = updateInfoModel.Email;
        //        user.PictureUrl = updateInfoModel.PictureUrl;

        //        await _context.SaveChangesAsync();

        //        return Ok(new ApiResponse<UserEntity>(true, "User account updated"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserEntity>(false, ex.Message));
        //    }
        //}
    }
}
