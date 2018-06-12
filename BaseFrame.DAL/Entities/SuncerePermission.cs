using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Entities
{
    public class SuncerePermission
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int Order { get; set; }

        public string Icon { get; set; }

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
