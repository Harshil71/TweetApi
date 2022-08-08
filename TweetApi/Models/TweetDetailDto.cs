using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class TweetDetailDto
    {
        /// <summary>
        /// Represents Tweet Message
        /// </summary>
        [Required]
        [MaxLength(144)]
        [Display(Name = "Tweet")]
        public string TweetMessage { get; set; }

        /// <summary>
        /// Represents Tweet User
        /// </summary>
        public User CreatedByUser { get; set; }

        /// <summary>
        /// Represents Tweet Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
