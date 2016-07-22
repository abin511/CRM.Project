using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Players
{
    /// <summary>
    /// 获取播放清单
    /// </summary>
    public class PlayListController : BaseApiController
    {
        readonly IRoomService _roomService = new RoomService();
        readonly IUserBaseService _userBaseService = new UserBaseService();
        /// <summary>
        /// 获取所有的播放清单 房间列表
        /// </summary>
        public HttpResponseMessage Get()
        {
            return base.Wrapper(() =>
            {
                var roomList = this._roomService.Get(m=>m.OnlineStatus && m.Status == (int)RoomStausEnum.None);
                var dataList = new List<ViewPlayer>();
                if (roomList != null && roomList.Any())
                {
                    var userIds = roomList.Select(m => m.UserId).ToList();
                    var userInfo = this._userBaseService.Get(m => userIds.Contains(m.ID));
                    roomList.ToList().ForEach(item =>
                    {
                        var user = userInfo.FirstOrDefault(m => m.ID == item.UserId)??new UserBase();
                        dataList.Add(new ViewPlayer()
                        {
                            roomid = item.ID,
                            nickname = user.NickName,
                            avatar = user.Avatar,
                            livetotalcount = item.TotalCount,
                            frontcover = item.Cover
                        });
                    });
                }
                return new Result<Object>
                {
                    Code = ResultEnum.Success,
                    Data = dataList
                };
            });
        }
    }
}
