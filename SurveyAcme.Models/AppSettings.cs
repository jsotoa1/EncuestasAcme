namespace SurveyAcme.Models
{
    public class AppSettings
    {
        public JWT JWT { get; set; }
    }

    public class JWT
    {
        public string JwtSecretKey { get; set; }
        public string IssuerName { get; set; }
        public string AudienceName { get; set; }
    }
}
