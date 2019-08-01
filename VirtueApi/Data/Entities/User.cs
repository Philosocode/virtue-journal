using System.Collections.Generic;

namespace VirtueApi.Data.Entities
{
    public class User
    {
        public User()
        {
            Virtues = new List<Virtue>();
            Entries = new List<Entry>();
        }
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public List<Virtue> Virtues { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
