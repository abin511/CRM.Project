using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Players
{
    /// <summary>
    ///直播
    /// </summary>
    public class LiveController : BaseApiController
    {
        readonly IRoomService _roomService = new RoomService();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public HttpResponseMessage Get(string token,string title,string cover)
        {
            return base.Wrapper<int>(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return _roomService.Live(userId, title, cover);
            });
        }
    }
}
