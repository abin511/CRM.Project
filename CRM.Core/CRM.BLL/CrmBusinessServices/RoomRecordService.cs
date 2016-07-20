﻿using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class RoomRecordService : IRoomRecordService
    {
        readonly IRoomService _roomService = new RoomService();
        readonly IUserFansService _userFansService = new UserFansService();
        readonly IUserBaseService _userBaseService = new UserBaseService();
        /// <summary>
        /// 用户进入直播间
        /// </summary>
        /// <returns></returns>
        public Result<Object> Join(int userId,int roomId)
        {
            var result = new Result<Object>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            if (roomId <= 0)
            {
                result.Msg = "房间ID无效";
                return result;
            }
            #endregion

            var now = DateTime.Now;

            #region 提交进入直播间记录
            var result1 = base.CurrentRepository.Add(new RoomRecord()
            {
                RoomId = roomId,
                UserId = userId,
                OnlineStatus = true,
                OnlineSeconds = 1,
                InsertTime = now,
                UpdateTime = now
            });
            if (result1 <= 0)
            {
                result.Msg = "进入直播间错误";
                return result;
            }
            var room = this._roomService.Get(m => m.ID == roomId).FirstOrDefault();
            if (room == null)
            {
                result.Msg = "直播间数据错误";
                return result;
            }
            room.OnlineCount += 1;
            room.TotalCount += 1;
            room.UpdateTime = now;
            var result2 = this._roomService.Update(room);
            if (result2.Code == ResultEnum.Error || result2.Data <= 0)
            {
                result.Msg = "直播间记录修改错误";
                return result;
            }
            #endregion

            #region 获取直播间数据
            //获取所有的粉丝
            var fansIds = this._userFansService.Get(m => m.UserId == room.UserId).Select(m=>m.UserId).ToList();
            var fansInfo =this._userBaseService.Get(m => fansIds.Contains(m.ID)).Select(m => new {UserId = m.ID, Avatar = m.Avatar}).ToList();
            var data = new
            {
                playurl = "",
                contribution = 1,
                count = 1,
                fans = fansInfo
            };
            #endregion
            result.Code= ResultEnum.Success;
            result.Data = data;
            return result;
        }
    }
}