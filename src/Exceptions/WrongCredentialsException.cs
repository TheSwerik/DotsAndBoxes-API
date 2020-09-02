namespace API.Exceptions
{
    public class WrongCredentialsException : HttpResponseException
    {
        public WrongCredentialsException()
        {
            Status = HttpResponseCode.Unauthorized;
            Value = "Username or Password is incorrect.";
            PrintStackTrace = false;
        }
    }
}