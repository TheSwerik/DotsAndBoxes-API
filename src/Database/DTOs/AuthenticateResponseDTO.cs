namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class AuthenticateResponseDTO
    {
        public bool IsAuthenticationSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}