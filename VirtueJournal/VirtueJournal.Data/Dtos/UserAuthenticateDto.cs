using System.ComponentModel.DataAnnotations;

namespace VirtueJournal.Data.Dtos
{
    public class UserAuthenticateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}