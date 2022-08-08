using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class UserDto
    {
        /// <summary>
        /// Represents User First Name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Represents User last Name
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Represents User Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

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

        /// <summary>
        /// Represents User Contact Number
        /// </summary>
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
    }
}
