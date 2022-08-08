using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class ForgetPasswordDto
    {
        /// <summary>
        /// Represents User Name
        /// </summary>
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Represents User Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Represents User Confirm Password
        /// </summary>
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
