namespace phoneApi.Models.Domain
{
    public class TokenInfo
    {
        public int Id { get; set; }
        public string Usernamee { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
