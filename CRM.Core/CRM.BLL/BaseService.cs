using System;
using System.Collections.Generic;
using System.Linq;
using CRM.DAL;
using CRM.IDAL;

namespace CRM.BLL
{
    public abstract class BaseCrmManageService<T> where T : class,new()
    {
        //在调用这个方法的时候必须给他赋值
        public IBaseRepository<T> CurrentRepository { get; set; }
        //为了职责单一的原则，将获取线程内唯一实例的DbSession的逻辑放到工厂里面去了
        public IDbSession _dbSession = DbSessionFactory.GetDbSession();

        //基类的构造函数 构造函数里面调用了此设置当前仓储的抽象方法
        public BaseCrmManageService()
        {
            SetCurrentRepository();
        }
        //构造方法实现赋值  约束子类必须实现这个抽象方法
        public abstract void SetCurrentRepository();

        //添加
        public int Add(T entity)
        {
            return CurrentRepository.Add(entity);
        }
        public int Add(List<T> entities)
        {
            return CurrentRepository.Add(entities);
        }
        //修改
        public int Update(T entity)
        {
            return CurrentRepository.Update(entity);
        }
        public int Update(List<T> entities)
        {
            return CurrentRepository.Update(entities);
        }
        public int Delete(T entity)
        {
            return CurrentRepository.Delete(entity);
        }
        public int Delete(List<T> entities)
        {
            return CurrentRepository.Delete(entities);
        }
        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return CurrentRepository.LoadEntities(wherelambda);
        }
        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            return CurrentRepository.LoadPagerEntities(pageSize, pageIndex, out total, whereLambda, isAsc, orderByLambda);
        }
    }
}
