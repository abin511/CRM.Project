using System;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Players
{
    /// <summary>
    /// 用户进入直播间
    /// </summary>
    public class JoinRoomController : BaseApiController
    {
        readonly IRoomRecordService _roomRecordService = new RoomRecordService();
        /// <summary>
        /// 用户进入直播间
        /// </summary>
        public HttpResponseMessage Get(string token,int roomId)
        {
            return base.Wrapper(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._roomRecordService.Join(userId, roomId);
            });
        }
    }
}
