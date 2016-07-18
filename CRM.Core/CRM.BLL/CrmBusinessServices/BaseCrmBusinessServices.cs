using System;
using System.Collections.Generic;
using System.Linq;
using CRM.DAL;
using CRM.IDAL;
using CRM.Model;

namespace CRM.BLL
{
    public abstract class BaseCrmBusinessServices<T> where T : class,new()
    {
        //在调用这个方法的时候必须给他赋值
        public IBaseRepository<T> CurrentRepository { get; set; }
        //为了职责单一的原则，将获取线程内唯一实例的DbSession的逻辑放到工厂里面去了
        public static ICrmBusinessDbSession DbSession = DbSessionFactory.GetCrmBusinessDbSession();
        //基类的构造函数 构造函数里面调用了此设置当前仓储的抽象方法
        protected BaseCrmBusinessServices()
        {
            SetCurrentRepository();
        }
        //构造方法实现赋值  约束子类必须实现这个抽象方法
        public abstract void SetCurrentRepository();

        //添加
        public Result<int> Add(T entity)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Add(entity),
                Code = ResultEnum.Success
            };
            return result;
        }
        public Result<int> Add(List<T> entities)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Add(entities),
                Code = ResultEnum.Success
            };
            return result;
        }
        //修改
        public Result<int> Update(T entity)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Update(entity),
                Code = ResultEnum.Success
            };
            return result;
        }
        public Result<int> Update(List<T> entities)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Update(entities),
                Code = ResultEnum.Success
            };
            return result;
        }
        public Result<int> Delete(T entity)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Delete(entity),
                Code = ResultEnum.Success
            };
            return result;
        }
        public Result<int> Delete(List<T> entities)
        {
            var result = new Result<int>
            {
                Data = CurrentRepository.Delete(entities),
                Code = ResultEnum.Success
            };
            return result;
        }
        //查询
        public IQueryable<T> Get(Func<T, bool> wherelambda)
        {
            return CurrentRepository.Get(wherelambda);
        }
        //分页
        public IQueryable<T> List<S>(int pageSize, int pageIndex, out int totalCount, Func<T, bool> whereLambda, Func<T, S> orderByLambda, bool isDesc = true)
        {
            return CurrentRepository.List(pageSize, pageIndex, out totalCount, whereLambda, orderByLambda,isDesc);
        }
    }
}
