namespace POC_Shared.Options
{
    public class JwtSecrets
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecretKey { get; set; }
    }
}
