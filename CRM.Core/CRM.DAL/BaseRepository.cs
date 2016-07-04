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
            base.DbContext = EFContextFactory.GetCrmManageDbContext();
        }
    }
    public abstract class CrmBusinessRepository<T> : BaseRepository<T> where T : class
    {
        protected CrmBusinessRepository()
        {
            base.DbContext = EFContextFactory.GetCrmBusinessDbContext();
        }
    }
    public abstract class BaseRepository<T> where T : class
    {
        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        public DbContext DbContext;

        //添加
        public int Add(T entity)
        {
            this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            return this.DbContext.SaveChanges();
        }
        public int Add(List<T> entities)
        {
            foreach (var entity in entities)
            {
                this.DbContext.Set<T>().Attach(entity);
                this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            }
            return this.DbContext.SaveChanges();
        }
        //修改
        public int Update(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
            this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return this.DbContext.SaveChanges();
        }
        public int Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                this.DbContext.Set<T>().Attach(entity);
                this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            }
            return this.DbContext.SaveChanges();
        }
        //删除
        public int Delete(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
            this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return this.DbContext.SaveChanges();
            //return true;
        }
        //删除
        public int Delete(List<T> entities)
        {
            foreach (var entity in entities)
            {
                this.DbContext.Set<T>().Attach(entity);
                this.DbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            }
            return this.DbContext.SaveChanges();
        }
        
        //查询
        public IQueryable<T> Get(Func<T, bool> wherelambda)
        {
            return this.DbContext.Set<T>().Where<T>(wherelambda).AsQueryable();
        }
        //查询
        public bool Exists(Func<T, bool> wherelambda)
        {
            return this.DbContext.Set<T>().Where<T>(wherelambda).Any();
        }
        //分页
        public IQueryable<T> List<S>(int pageSize, int pageIndex, out int total,Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = this.DbContext.Set<T>().Where<T>(whereLambda);
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
