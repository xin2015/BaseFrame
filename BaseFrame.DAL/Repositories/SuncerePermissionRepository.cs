using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Repositories
{
    public class SuncerePermissionRepository : Repository<SuncerePermission>
    {
        public SuncerePermissionRepository(FluentModel db) : base(db)
        {
        }

        public IQueryable<SuncerePermission> Query(DateTime? startTime, DateTime? endTime, string keyword)
        {
            IQueryable<SuncerePermission> query = GetAll();
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
                query = query.Where(o => o.Name.Contains(keyword) || o.Controller.Contains(keyword) || o.Action.Contains(keyword));
            }
            return query;
        }

        public IQueryable<SuncerePermission> Query(int[] ids)
        {
            return _db.SuncerePermissions.Where(o => ids.Contains(o.Id));
        }

        public bool IsExist(string controller, string action)
        {
            return GetAll().FirstOrDefault(o => o.Controller == controller && o.Action == action) != null;
        }
        public SuncerePermission FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(o => o.Id == id);
        }
    }
}
