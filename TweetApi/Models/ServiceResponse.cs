namespace TweetApi.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; } = true;

        public string Error { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
