namespace SurveyAcme.Models.Inputs
{
    public class SurveyCrudInsertRequest
    {
        public int SurveyId { get; set; }
        public List<SurveyFieldInsert> Fields { get; set; } = new List<SurveyFieldInsert>();
    }

    public class SurveyFieldInsert
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Required { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
