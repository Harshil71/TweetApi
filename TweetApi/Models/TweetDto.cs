using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class TweetDto
    {
        /// <summary>
        /// Represents Tweet Message
        /// </summary>
        [Required]
        [MaxLength(144)]
        [Display(Name = "Tweet")]
        public string TweetMessage { get; set; }

    }
}
