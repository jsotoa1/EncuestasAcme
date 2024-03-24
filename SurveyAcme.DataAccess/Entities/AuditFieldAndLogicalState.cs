using SurveyAcme.Utilities.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAcme.DataAccess.Entities
{
    public class AuditFieldAndLogicalState
    {
        public AuditFieldAndLogicalState()
        {
            CreationDate = TimeZoneHelper.Now;
            IsActive = true;
            Deleted = false;
        }

        [Column("date_created")]
        public DateTime CreationDate { get; set; }

        [Column("user_created")]
        [MaxLength(50)]
        public string CreationUser { get; set; }

        [Column("date_modified")]
        public DateTime? ModificationDate { get; set; }

        [Column("user_modified")]
        [MaxLength(50)]
        public string? ModificationUser { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
