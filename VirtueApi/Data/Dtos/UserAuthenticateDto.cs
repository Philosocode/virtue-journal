using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Dtos
{
    public class UserAuthenticateDto
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}