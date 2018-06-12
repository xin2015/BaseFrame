using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Repositories
{
    public class SuncereAuditLogRepository : Repository<SuncereAuditLog>
    {
        public SuncereAuditLogRepository(FluentModel db) : base(db)
        {
        }

        public IQueryable<SuncereAuditLog> Query(DateTime? startTime, DateTime? endTime, string keyword)
        {
            IQueryable<SuncereAuditLog> query = GetAll();
            if (startTime.HasValue)
            {
                query = query.Where(o => o.CreationTime >= startTime.Value);
            }
            if (endTime.HasValue)
            {
                query = query.Where(o => o.CreationTime <= endTime.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.Url.Contains(keyword) || o.HostName.Contains(keyword) || o.UserName.Contains(keyword));
            }
            return query;
        }

        public IQueryable<SuncereAuditLog> Query(int[] idArray)
        {
            return GetAll().Where(o => idArray.Contains(o.Id));
        }
    }
}
