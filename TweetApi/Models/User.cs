using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class User
    {
        /// <summary>
        /// Represents User Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Represents User First Name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Represents User Last Name
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
        /// Represents User Contact Number
        /// </summary>
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Represents User Password Hash
        /// </summary>
        [Display(AutoGenerateField = false)]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Represents Password Salt
        /// </summary>
        [Display(AutoGenerateField = false)]
        public byte[] PasswordSalt { get; set; }
    }
}
