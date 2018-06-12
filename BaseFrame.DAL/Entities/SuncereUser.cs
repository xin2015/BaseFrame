using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Entities
{
    public class SuncereUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public string LastLoginHostAddress { get; set; }

        public bool Status { get; set; }

        public bool IsStatic { get; set; }

        public string Remark { get; set; }

        public DateTime CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public DateTime LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        private IList<SuncereRole> _suncereRoles = new List<SuncereRole>();
        public virtual IList<SuncereRole> SuncereRoles
        {
            get
            {
                return _suncereRoles;
            }
        }
    }
}
