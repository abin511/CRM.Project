using System;
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
        public ICrmManageDbSession _dbSession = DbSessionFactory.GetCrmManageDbSession();

        //基类的构造函数 构造函数里面调用了此设置当前仓储的抽象方法
        public BaseCrmManageService()
        {
            SetCurrentRepository();  
        }
        //构造方法实现赋值  约束子类必须实现这个抽象方法
        public abstract void SetCurrentRepository();

        //添加
        public T AddEntities(T entity)
        {
            //如果在这里操作多个表的话，实现批量的操作
            //调用T对应的仓储来添加
            var addentity = CurrentRepository.AddEntities(entity);
            return addentity;
        }
        //修改
        public bool UpdateEntities(T entity)
        {
            var updateEntity =  CurrentRepository.UpdateEntities(entity);
            return updateEntity;
        }
        //修改
        public bool DeleteEntities(T entity)
        {
            var deleteEntity= CurrentRepository.DeleteEntities(entity);
            return deleteEntity;
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
