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
        public static ICrmBusinessDbSession GetCrmBusinessDbSession()
        {
            ICrmBusinessDbSession dbSession = CallContext.GetData("CrmBusinessDbSession") as ICrmBusinessDbSession;
            if (dbSession == null)
            {
                dbSession = new CrmBusinessDbSession();
                CallContext.SetData("CrmBusinessDbSession", dbSession);
            }
            return dbSession;
        }
        /// <summary>
        /// 保证了线程内DbSession实例唯一
        /// </summary>
        /// <returns></returns>
        public static ICrmManageDbSession GetCrmManageDbSession()
        {
            ICrmManageDbSession dbSession = CallContext.GetData("CrmManageDbSession") as ICrmManageDbSession;
            if (dbSession == null)
            {
                dbSession = new CrmManageDbSession();
                CallContext.SetData("CrmManageDbSession", dbSession);
            }
            return dbSession;
        }
    }
}
