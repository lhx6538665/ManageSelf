using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseData<T> where T : class
    {

        protected DataContext DB = new DataContext();

        public IQueryable<T> Entities { get { return DB.Set<T>(); } }

        public T Add(T entity, bool isSave = true)
        {
            DB.Set<T>().Add(entity);
            if (isSave) DB.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return DB.Set<T>().Count(predicate);
        }

        public bool Update(T entity, bool isSave = true)
        {
            DB.Set<T>().Attach(entity);
            DB.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return isSave ? DB.SaveChanges() > 0 : true;
        }

        public bool Delete(T entity, bool isSave = true)
        {
            DB.Set<T>().Attach(entity);
            DB.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return isSave ? DB.SaveChanges() > 0 : true;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return DB.Set<T>().Any(anyLambda);
        }

        public T Find(int ID)
        {
            return DB.Set<T>().Find(ID);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = DB.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public int Save()
        {
            return DB.SaveChanges();
        }
        public IQueryable<T> PageList(IQueryable<T> entitys, int pageIndex, int pageSize)
        {
            return entitys.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
