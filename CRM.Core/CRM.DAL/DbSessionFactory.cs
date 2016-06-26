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
        public static ICrmManageDbSession GetCrmManageDbSession()
        {
            ICrmManageDbSession _dbSession = CallContext.GetData("CrmManageDbSession") as ICrmManageDbSession;
            if (_dbSession == null)
            {
                _dbSession = new CrmManageDbSession();
                CallContext.SetData("CrmManageDbSession", _dbSession);
            }
            return _dbSession;
        }
        /// <summary>
        /// 保证了线程内DbSession实例唯一
        /// </summary>
        /// <returns></returns>
        public static ICrmBusinessDbSession GetCrmBusinessDbSession()
        {
            ICrmBusinessDbSession _dbSession = CallContext.GetData("CrmBusinessDbSession") as ICrmBusinessDbSession;
            if (_dbSession == null)
            {
                _dbSession = new CrmBusinessDbSession();
                CallContext.SetData("CrmBusinessDbSession", _dbSession);
            }
            return _dbSession;
        }
    }
}
