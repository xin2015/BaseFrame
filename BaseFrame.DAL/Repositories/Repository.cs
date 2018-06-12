using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.DAL.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        protected FluentModel _db;
        public Repository(FluentModel db)
        {
            _db = db;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _db.GetAll<TEntity>();
        }
    }
}
