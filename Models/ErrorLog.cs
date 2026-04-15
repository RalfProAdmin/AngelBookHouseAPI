namespace EkartAPI.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Module { get; set; }
        public string ErrorType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
