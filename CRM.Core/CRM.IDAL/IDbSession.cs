using System.Data.Common;

namespace CRM.IDAL
{
    //添加接口，起约束作用
    public partial interface IDbSession
    {
        int SaveChanges();
        int ExcuteSql(string strSql, DbParameter[] parameters);
    }
}
