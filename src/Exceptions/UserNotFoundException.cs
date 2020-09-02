namespace API.Exceptions
{
    public class UserNotFoundException : HttpResponseException
    {
        public UserNotFoundException(string username)
        {
            Status = HttpResponseCode.NotFound;
            Value = $"User with this {username} is not found.";
            PrintStackTrace = false;
        }
    }
}