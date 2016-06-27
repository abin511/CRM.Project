using System.Runtime.Remoting.Messaging;
using CRM.IDAL;

namespace CRM.DAL
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 保证了线程内DbSession实例唯一
        /// </summary>
        /// <returns></returns>
        public static IDbSession GetDbSession()
        {
            IDbSession _dbSession = CallContext.GetData("CrmDbSession") as IDbSession;
            if (_dbSession == null)
            {
                _dbSession = new DbSession();
                CallContext.SetData("CrmDbSession", _dbSession);
            }
            return _dbSession;
        }
    }
}
