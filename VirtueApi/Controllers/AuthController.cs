using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Extensions;
using VirtueApi.Services;
using VirtueApi.Services.Repositories;
using VirtueApi.Shared;

// FROM: https://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
namespace VirtueApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserAuthenticateDto userDto)
        {
            var userName = userDto.UserName.ToLower();
            var password = userDto.Password;
            
            var user = await _unitOfWork.Auth.AuthenticateAsync(userName, password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userDto)
        {
            userDto.UserName = userDto.FirstName.ToLower();
            userDto.Email = userDto.Email.ToLower();
            
            if (await _unitOfWork.Auth.UserNameInUseAsync(userDto.UserName))
                return StatusCode(409, "Username already in use.");
            
            if (await _unitOfWork.Auth.EmailInUseAsync(userDto.Email))
                return StatusCode(409, "Email already in use.");
                    
            var user = _mapper.Map<User>(userDto);
            
            try 
            {
                await _unitOfWork.Auth.CreateAsync(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = this.GetCurrentUserId();
            
            var userFromRepo = await _unitOfWork.Auth.GetByIdAsync(userId);
            var userToReturn = _mapper.Map<UserGetDto>(userFromRepo);
            
            return Ok(userToReturn);
        }
        
        [HttpGet("user/id")]
        public IActionResult GetUserId()
        {
            var userId = this.GetCurrentUserId();
            
            return Ok(userId);
        }


        [HttpPatch("user")]
        public IActionResult Update(UserUpdateDto userDto)
        {
            return BadRequest("Not implemented yet");
        }

        [HttpDelete("user")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.Auth.DeleteAsync(id);
            return Ok();
        }
    }
}
