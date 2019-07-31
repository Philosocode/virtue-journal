using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}