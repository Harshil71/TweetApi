using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class LoginDto
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
