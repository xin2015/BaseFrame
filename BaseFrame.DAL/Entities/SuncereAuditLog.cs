using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Entities
{
    public class SuncereAuditLog
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Url { get; set; }

        public string Referrer { get; set; }

        public string HostName { get; set; }

        public string HostAddress { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
