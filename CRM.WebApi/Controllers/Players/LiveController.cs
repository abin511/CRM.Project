using System.Net.Http;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Players
{
    /// <summary>
    ///用户直播
    /// </summary>
    public class LiveController : BaseApiController
    {
        readonly IRoomService _roomService = new RoomService();
        /// <summary>
        /// 用户直播 推流
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Live(string token,string title,string cover)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._roomService.Live(userId, title, cover);
            },token);
        }
    }
}
