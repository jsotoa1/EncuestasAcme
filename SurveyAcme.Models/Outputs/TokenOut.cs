using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyAcme.Models.Outputs
{
    public class TokenOut
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserDataOut UserData { get; set; }
    }

    public class UserDataOut
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Description { get; set; }
        public int RolUserId { get; set; }
        public int ExpirationTime { get; set; }
        public int RoleCompanyId { get; set; }
        public int CompanyId { get; set; }
        public bool Activate_2fa { get; set; }
    }
}
