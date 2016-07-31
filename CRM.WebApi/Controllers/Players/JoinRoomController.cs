using System;
using System.Net.Http;
using System.Web.Mvc;
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
        [HttpPost]
        public HttpResponseMessage Join(string token,int roomId)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._roomRecordService.Join(userId, roomId);
            },token);
        }
    }
}
