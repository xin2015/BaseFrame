using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Entities
{
    public class SuncereRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public bool IsStatic { get; set; }

        public string Remark { get; set; }

        public DateTime CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public DateTime LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        private IList<SuncereUser> _suncereUsers = new List<SuncereUser>();
        public virtual IList<SuncereUser> SuncereUsers
        {
            get
            {
                return _suncereUsers;
            }
        }

        private IList<SuncerePermission> _suncerePermissions = new List<SuncerePermission>();
        public virtual IList<SuncerePermission> SuncerePermissions
        {
            get
            {
                return _suncerePermissions;
            }
        }
    }
}
