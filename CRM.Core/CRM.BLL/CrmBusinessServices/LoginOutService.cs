using System;
using System.Linq;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class LoginOutService : ILoginOutService
    {
        readonly IRoomService _roomService = new RoomService();
        readonly IRoomRecordService _roomRecordService = new RoomRecordService();
        /// <summary>
        /// 主播登出
        /// </summary>
        /// <returns></returns>
        public Result<int> LoginOutByLive(int userId,int roomId)
        {
            var result = new Result<int>();
            var room = this._roomService.Get(m => m.UserId == userId && m.ID == roomId && m.Status == (int)RoomStausEnum.Live).FirstOrDefault();
            if (room == null)
            {
                result.Msg = "直播间信息无效";
                return result;
            }
            var now = DateTime.Now;
            room.Status = (int)RoomStausEnum.Lazy;
            room.OnlineCount = 0;
            room.UpdateTime = now;
            result = this._roomService.Update(room);

            var records = this._roomRecordService.Get(m => m.RoomId == room.ID && m.IsOnline).ToList();
            if (!records.IsNullOrEmpty())
            {
                records.ForEach(item =>
                {
                    TimeSpan ts = item.InsertTime - now;
                    item.OnlineMinutes = ts.Days*24*60 + ts.Hours*60 + ts.Minutes;
                    item.UpdateTime = now;
                    item.IsOnline = false;
                });
                this._roomRecordService.Update(records);
            }
            return result;
        }

        /// <summary>
        /// 观众登出
        /// </summary>
        /// <returns></returns>
        public Result<int> LoginOutByUser(int userId, int roomId,int recordId)
        {
            var result = new Result<int>();
            var records = this._roomRecordService.Get(m => m.UserId == userId && m.RoomId == roomId && m.ID == recordId && m.IsOnline).FirstOrDefault();
            if (records == null)
            {
                result.Msg = "直播间记录无效";
                return result;
            }
            var now = DateTime.Now;
            TimeSpan ts = records.InsertTime - now;
            records.OnlineMinutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
            records.UpdateTime = now;
            records.IsOnline = false;
            result = this._roomRecordService.Update(records);

            var room = this._roomService.Get(m => m.ID == records.RoomId).FirstOrDefault();
            if (room == null)
            {
                result.Msg = "直播间信息无效";
                return result;
            }
            room.OnlineCount -= 1;
            room.UpdateTime = now;
            result = this._roomService.Update(room);
            return result;
        }
    }
}