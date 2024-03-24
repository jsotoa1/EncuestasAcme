using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAcme.DataAccess.Entities
{
    [Table("survey")]
    public class Survey : AuditFieldAndLogicalState
    {

        [Column("survey_id")]
        public int Id { get; set; }

        [Column("survey_name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("survey_description")]
        [MaxLength(250)]
        public string Description { get; set; }

        [Column("survey_link")]
        [MaxLength(int.MaxValue)]
        public string? Link { get; set; }

        public List<SurveyField> SurveyField { get; set; }
    }
}
