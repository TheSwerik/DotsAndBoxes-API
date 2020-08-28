namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class AuthenticateResponse
    {
        public bool IsAuthenticationSuccessful { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"{{ Successfull: {IsAuthenticationSuccessful} | Token: {Token} | ErrorMessage: {ErrorMessage} }}";
        }
    }
}