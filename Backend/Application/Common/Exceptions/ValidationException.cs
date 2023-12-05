namespace Application.Common.Exceptions
{
    public class ValidationException:Exception
    {
        public Object Details { get; set; }
        public ValidationException(string message,Object details) : base(message)
        {
            Details = details;
        }
    }
}
