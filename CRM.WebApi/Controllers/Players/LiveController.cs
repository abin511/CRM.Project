using System.Net.Http;
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
        public HttpResponseMessage Get(string token,string title,string cover)
        {
            return base.WrapperResponse(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._roomService.Live(userId, title, cover);
            });
        }
    }
}
