using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IBaseService<T> where T:class,new()
    {
        //添加
        Result<int> Add(T entity);
        Result<int> Add(List<T> entities);
        //修改
        Result<int> Update(T entity);
        Result<int> Update(List<T> entities);
        //删除
        Result<int> Delete(T entity);
        Result<int> Delete(List<T> entities);
        //查询
        IQueryable<T> Get(Func<T, bool> wherelambda);
        //List分页
        IQueryable<T> List<S>(int pageSize, int pageIndex,out int totalCount, Func<T, bool> whereLambda, Func<T, S> orderByLambda, bool isDesc = true);
    }
}
