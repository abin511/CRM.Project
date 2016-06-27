using System;
using System.Collections.Generic;
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
        public int Add(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            return Db.SaveChanges();
        }
        public int Add(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Db.Set<T>().Attach(entity);
                Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            }
            return Db.SaveChanges();
        }
        //修改
        public int Update(T entity)
        {
            Db.Set<T>().Attach(entity);
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return Db.SaveChanges();
        }
        public int Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Db.Set<T>().Attach(entity);
                Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            }
            return Db.SaveChanges();
        }
        //删除
        public int Delete(T entity)
        {
            Db.Set<T>().Attach(entity);
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return Db.SaveChanges();
            //return true;
        }
        //删除
        public int Delete(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Db.Set<T>().Attach(entity);
                Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            }
            return Db.SaveChanges();
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
