using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Repositories
{
    public class SuncereRoleRepository : Repository<SuncereRole>
    {
        public SuncereRoleRepository(FluentModel db) : base(db)
        {
        }

        public IQueryable<SuncereRole> Query(DateTime? startTime, DateTime? endTime, string keyword)
        {
            IQueryable<SuncereRole> query = GetAll();
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
                query = query.Where(o => o.Name.Contains(keyword));
            }
            return query;
        }

        public IQueryable<SuncereRole> Query(int[] idArray)
        {
            return GetAll().Where(o => idArray.Contains(o.Id));
        }

        public bool IsExist(string name)
        {
            return GetAll().FirstOrDefault(o => o.Name == name) != null;
        }

        public SuncereRole FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(o => o.Id == id);
        }
    }
}
