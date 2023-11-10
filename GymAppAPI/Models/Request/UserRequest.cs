using System.ComponentModel.DataAnnotations;

namespace GymAppAPI.Models.Request
{
    public class UserRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string nickName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
