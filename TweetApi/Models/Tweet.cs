using System.ComponentModel.DataAnnotations;

namespace TweetApi.Models
{
    public class Tweet
    {
        /// <summary>
        /// Represents Tweet Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Represents Tweet Message
        /// </summary>
        [Required]
        [MaxLength(144)]
        [Display(Name ="Tweet")]
        public string TweetMessage { get; set; }

        /// <summary>
        /// Represents Tweet User
        /// </summary>
        public User CreatedByUser { get; set; }

        /// <summary>
        /// Represents Tweet Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Represents Tweet Updated
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
