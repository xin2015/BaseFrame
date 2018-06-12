using BaseFrame.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Repositories
{
    public class SuncereUserRepository : Repository<SuncereUser>
    {
        public SuncereUserRepository(FluentModel db) : base(db)
        {

        }

        public IQueryable<SuncereUser> Query(DateTime? startTime, DateTime? endTime, string keyword)
        {
            IQueryable<SuncereUser> query = GetAll();
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
                query = query.Where(o => o.UserName.Contains(keyword));
            }
            return query;
        }

        public IQueryable<SuncereUser> Query(int[] idArray)
        {
            return GetAll().Where(o => idArray.Contains(o.Id));
        }

        public SuncereUser FirstOrDefault(string userName, bool status)
        {
            return GetAll().FirstOrDefault(o => o.UserName == userName && o.Status == status);
        }

        public SuncereUser FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(o => o.Id == id);
        }

        public bool IsExist(string userName)
        {
            return GetAll().FirstOrDefault(o => o.UserName == userName) != null;
        }
    }
}
