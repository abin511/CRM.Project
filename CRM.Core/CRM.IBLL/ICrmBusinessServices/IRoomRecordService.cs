using System;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IRoomRecordService
    {
        /// <summary>
        /// 用户进入直播间
        /// </summary>
        /// <returns></returns>
        Result<Object> Join(int userId,int roomId);
    }
}
