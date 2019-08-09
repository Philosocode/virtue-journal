using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;
using VirtueJournal.Data.Repositories;
using VirtueJournal.Shared;

// FROM: https://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
namespace VirtueJournal.API.Controllers
{
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserAuthenticateDto userDto)
        {
            // Input Validation
            var userName = userDto.UserName?.ToLower();
            var email = userDto.Email?.ToLower();

            if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(email))
                return StatusCode(422, "Username or email is required.");
            
            var password = userDto.Password;
            
            if (string.IsNullOrWhiteSpace(password))
                return StatusCode(422, "Password cannot be empty.");
            
            var user = await _unitOfWork.Auth.AuthenticateAsync(userName, email, password);
            
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = _unitOfWork.Auth.GenerateToken(user.UserId, _appSettings.Secret);

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
            
            await _unitOfWork.Auth.RegisterAsync(user, userDto.Password);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not create user.");

            var userToReturn = _mapper.Map<UserGetDto>(user);

            return StatusCode(201, userToReturn);
        }
    }
}
