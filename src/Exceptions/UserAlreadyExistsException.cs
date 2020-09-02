namespace API.Exceptions
{
    public class UserAlreadyExistsException : HttpResponseException
    {
        public UserAlreadyExistsException(string username)
        {
            Status = HttpResponseCode.Conflict;
            Value = $"User with this {username} already Exists.";
            PrintStackTrace = false;
        }
    }
}