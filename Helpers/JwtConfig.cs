namespace jwt.Helpers
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audence { get; set; }
        public string DurationInDays { get; set; }
    }
}
