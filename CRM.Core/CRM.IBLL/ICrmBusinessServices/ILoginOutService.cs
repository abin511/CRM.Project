using System.Collections.Generic;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface ILoginOutService
    {
        /// <summary>
        /// 主播登出
        /// </summary>
        /// <returns></returns>
        Result<int> LoginOutByLive(int userId,int roomId);
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        Result<int> LoginOutByUser(int userId, int roomId,int recordId);
    }
}
