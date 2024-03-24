namespace SurveyAcme.Models.Inputs
{
    public class SurveyCreateIn
    {
        public int Id { get; set; }
        public string SurveyName { get; set; } = string.Empty;
        public string SurveyDescription { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public List<SurveyField> ListSurveyFields { get; set;} = new List<SurveyField>();
    }

    public class SurveyField
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public char Required { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
