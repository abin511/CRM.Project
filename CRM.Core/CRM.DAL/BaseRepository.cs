using System;
using System.Data.Entity;
using System.Linq;

namespace CRM.DAL
{
    public abstract class CrmManageRepository<T>:BaseRepository<T> where T : class
    {
        protected CrmManageRepository()
        {
            base.Db = EFContextFactory.GetCrmManageDbContext();
        }
    }
    public abstract class CrmBusinessRepository<T> : BaseRepository<T> where T : class
    {
        protected CrmBusinessRepository()
        {
            base.Db = EFContextFactory.GetCrmBusinessDbContext();
        }
    }
    public abstract class BaseRepository<T> where T : class
    {
        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        public DbContext Db;

        //添加
        public T AddEntities(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            //Db.SaveChanges();
            return entity;
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            Db.Set<T>().Attach(entity);
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //Db.Entry<T>(entity).State= EntityState.Unchanged;
            //return Db.SaveChanges() > 0;
            return true;
        }
        
        //修改
        public bool DeleteEntities(T entity)
        {
            Db.Set<T>().Attach(entity);
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }

        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return Db.Set<T>().Where<T>(wherelambda).AsQueryable();
        }

        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = Db.Set<T>().Where<T>(whereLambda);
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
