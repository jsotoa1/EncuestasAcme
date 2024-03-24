using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAcme.DataAccess.Entities
{
    [Table("survey_field")]
    public class SurveyField : AuditFieldAndLogicalState
    {
        [Column("survey_field_id")]
        public int Id { get; set; }

        [Column("survey_field_name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("survey_field_title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Column("survey_field_required")]
        public char Required { get; set; }

        [Column("survey_field_Type")]
        [MaxLength(20)]
        public string Type { get; set; }

        [Column("survey_id")]
        public Survey Survey { get; set; }
        public List<SurveyFieldData> SurveyFieldData { get; set; }
    }
}
