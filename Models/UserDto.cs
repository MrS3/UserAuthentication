using System.ComponentModel.DataAnnotations;

namespace UserAuth.API.Models
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}