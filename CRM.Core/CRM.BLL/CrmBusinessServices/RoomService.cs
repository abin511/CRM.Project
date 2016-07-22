using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class RoomService : IRoomService
    {
        /// <summary>
        /// 主播推流
        /// </summary>
        /// <returns></returns>
        public Result<int> Live(int userId,string title,string cover)
        {
            var result = new Result<int>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            if (string.IsNullOrEmpty(title))
            {
                result.Msg = "直播主题不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(cover))
            {
                result.Msg = "直播封面不能为空";
                return result;
            }
            #endregion

            var now = DateTime.Now;
            var model = base.CurrentRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (model == null)
            {
                var result1 = base.CurrentRepository.Add(new Room()
                {
                    UserId = userId,
                    Title = title,
                    Cover = cover,
                    OnlineStatus = true,
                    OnlineCount = 1,
                    TotalCount = 1,
                    Contribution = 0,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (result1 <= 0)
                {
                    result.Msg = "主播推流信息增加失败";
                    return result;
                }
                result.Data = result1;
            }
            else
            {
                if (model.Status == (int) RoomStausEnum.Lock)
                {
                    result.Msg = "直播间已经被锁定";
                    return result;
                }
                model.OnlineStatus = true;
                model.OnlineCount += 1;
                model.TotalCount += 1;
                model.Contribution += 1;
                model.UpdateTime = now;
                var result2 = base.CurrentRepository.Update(model);
                if (result2 <= 0)
                {
                    result.Msg = "主播推流信息修改失败";
                    return result;
                }
                result.Data = model.ID;
            }
            result.Code= ResultEnum.Success;
            return result;
        }
    }
}