using System;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Players
{
    /// <summary>
    /// 获取播放清单
    /// </summary>
    public class JoinRoomController : BaseApiController
    {
        readonly IRoomRecordService _roomRecordService = new RoomRecordService();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public HttpResponseMessage Get(string token,int roomId)
        {
            return base.Wrapper<Object>(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return _roomRecordService.Join(userId, roomId);
            });
        }
    }
}
