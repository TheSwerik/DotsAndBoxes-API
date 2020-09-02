namespace API.Exceptions
{
    public class UserNotFoundException : HttpResponseException
    {
        public UserNotFoundException(string username)
        {
            Status = 404;
            Value = username;
        }
    }
}