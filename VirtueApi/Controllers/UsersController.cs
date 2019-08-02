using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Repositories;
using VirtueApi.Extensions;

namespace VirtueApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public IActionResult GetUserIdDev()
        {
            var userId = this.GetCurrentUserId();
            
            return Ok(userId);
        }

        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto updates)
        {
            var userId = this.GetCurrentUserId();
            var toUpdate = await _unitOfWork.Auth.GetByIdAsync(userId);
            
            // Update UserName
            var newUserName = updates.UserName;
            if (!string.IsNullOrWhiteSpace(newUserName))
            {
                // UserName has changed so check if the new UserName is already taken
                if (await _unitOfWork.Auth.UserNameInUseAsync(newUserName))
                    return StatusCode(409, "Username already in use.");

                toUpdate.UserName = newUserName;
            }
            
            // Update Email
            var newEmail = updates.Email;
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                // Email has changed so check if the new Email is already taken
                if (await _unitOfWork.Auth.UserNameInUseAsync(newEmail))
                    return StatusCode(409, "Email already in use.");

                toUpdate.Email = newEmail;
            }
            
            // Update first name and last name
            var newFirstName = updates.FirstName;
            if (!string.IsNullOrWhiteSpace(newFirstName))
                toUpdate.FirstName = newFirstName;
            var newLastName = updates.LastName;
            if (!string.IsNullOrWhiteSpace(newLastName))
                toUpdate.LastName = newLastName;
            
            // Update password
            var newPassword = updates.Password;
            if (!string.IsNullOrEmpty(newPassword))
                _unitOfWork.Auth.UpdatePassword(toUpdate, newPassword);
            
            // Save
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not update user {userId}");

            // If password was changed, make user login again
            if (!string.IsNullOrWhiteSpace(newPassword))
                return Challenge();
            
            return NoContent();
        }
        
        [HttpDelete("user")]
        // TODO: Make User Delete for admin
        // TODO: What happens after the user deletes their account?
        public async Task<IActionResult> Delete(int userId)
        {
            await _unitOfWork.Auth.DeleteAsync(userId);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete user {userId}");
            
            return NoContent();
        }
    }
}