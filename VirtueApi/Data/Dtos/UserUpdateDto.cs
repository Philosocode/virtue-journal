using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Dtos
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string UserName { get; set; }
        
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}