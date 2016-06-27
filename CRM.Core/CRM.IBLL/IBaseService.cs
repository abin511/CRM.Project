using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.IBLL
{
    public partial interface IBaseService<T> where T:class,new()
    {
        //添加
        int Add(T entity);
        int Add(List<T> entities);
        //修改
        int Update(T entity);
        int Update(List<T> entities);
        //删除
        int Delete(T entity);
        int Delete(List<T> entities);
        //查询
        IQueryable<T> LoadEntities(Func<T, bool> wherelambda);
        //分页
        IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
    }
}
