using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace CRM.DAL
{
    public partial class BaseRepository<T> where T : class
    {

        //EF上下文的实例保证，线程内唯一
        //实例化EF框架
        //DataModelContainer db = new DataModelContainer();

        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        private DbContext db = EFContextFactory.GetCurrentDbContext();

        //添加
        public T AddEntities(T entity)
        {
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            //db.SaveChanges();
            return entity;
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //db.Entry<T>(entity).State= EntityState.Unchanged;
            //return db.SaveChanges() > 0;
            return true;
        }
        
        //修改
        public bool DeleteEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            //return db.SaveChanges() > 0;
            return true;
        }

        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return db.Set<T>().Where<T>(wherelambda).AsQueryable();
        }

        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = db.Set<T>().Where<T>(whereLambda);
            total = tempData.Count();
            //排序获取当前页的数据
            if (isAsc)
            {
                tempData = tempData.OrderBy<T, S>(orderByLambda);
            }
            else
            {
                tempData = tempData.OrderByDescending<T, S>(orderByLambda);
            }
            return tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
        }
    }
}
