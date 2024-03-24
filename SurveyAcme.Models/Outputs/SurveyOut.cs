namespace SurveyAcme.Models.Outputs
{
    public class SurveyOut
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public List<Field> Fields { get; set; } = new List<Field>();
    }

    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Required { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<FieldData> Data { get; set; } = new List<FieldData>();
    }

    public class FieldData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
