using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.IDAL
{
    /// <summary>
    /// 基仓储实现的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:class,new()
    {
        //添加
        int Add(T entity);
        //添加
        int Add(List<T> entities);
        //修改
        int Update(T entity);
        //修改
        int Update(List<T> entities);
        //修改
        int Delete(T entity);
        //修改
        int Delete(List<T> entities);
        //是否存在
        bool Exists(Func<T, bool> wherelambda);
        //查询
        IQueryable<T> Get(Func<T, bool> wherelambda);
        //List分页
        IQueryable<T> List<S>(int pageSize, int pageIndex, out int totalCount, Func<T, bool> whereLambda,Func<T, S> orderByLambda, bool isDesc = true);
    }
}