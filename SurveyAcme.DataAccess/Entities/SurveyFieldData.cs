using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAcme.DataAccess.Entities
{
    [Table("survey_field_data")]
    public class SurveyFieldData : AuditFieldAndLogicalState
    {
        [Column("survey_field_data_id")]
        public int Id { get; set; }

        [Column("survey_field_data_name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("survey_field_id")]
        public SurveyField SurveyField { get; set; }
    }
}