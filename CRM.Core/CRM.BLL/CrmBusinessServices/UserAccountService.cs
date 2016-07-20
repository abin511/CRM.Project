using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserAccountService : IUserAccountService
    {
        readonly IGiftService _giftService = new GiftService();
        readonly IMoneyRecordService _moneyRecordService = new MoneyRecordService();
        readonly IGoldRecordService _goldRecordService = new GoldRecordService();
        readonly IRoomService _roomService = new RoomService();
        /// <summary>
        /// 用户充值 增加金币
        /// </summary>
        /// <returns></returns>
        public Result<int> Recharge(int userId,decimal amount)
        {
            var result = new Result<int>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            if (amount < 1)
            {
                result.Msg = "充值金额必须大于1元";
                return result;
            }
            #endregion

            var userAccount = this.CurrentRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (userAccount == null)
            {
                result.Msg = "用户资金信息无效";
                return result;
            }
            
            var now = DateTime.Now;
            //用户账户变动
            var gold = userAccount.Gold;
            var rechare = (int)Math.Ceiling(amount) * 10;
            userAccount.Gold += rechare;
            userAccount.Contribution += rechare;
            userAccount.UpdateTime = now;
            var result1 = this.CurrentRepository.Update(userAccount);
            if (result1 <= 0)
            {
                result.Msg = "用户充值失败";
                return result;
            }
            //资金变动记录
            var result2 = this._moneyRecordService.Add(new MoneyRecord()
            {
                UserId = userId,
                ChangeType = (int)MoneyChangeTypeEnum.Recharge
            });
            //金币变动记录
            var result3 = this._goldRecordService.Add(new GoldRecord()
            {
                UserId = userId,
                ChangeType = (int)GoldChangeTypeEnum.Increase,
                GoldBefore = gold,
                GoldAfter = userAccount.Gold,
                Remark = $"用户充值，增加金币{rechare}",
                InsertTime = now,
                UpdateTime = now
            });
            result.Data = userAccount.Gold;
            result.Code= ResultEnum.Success;
            return result;
        }
        /// <summary>
        /// 用户购买礼物 赠送主播
        /// </summary>
        /// <returns></returns>
        public Result<int> Cost(int userId,int roomId,int giftId)
        {
            var result = new Result<int>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            if (roomId <= 0)
            {
                result.Msg = "直播房间号无效";
                return result;
            }
            if (giftId <= 0)
            {
                result.Msg = "礼物信息无效";
                return result;
            }
            #endregion

            #region 数据验证
            var giftList =_giftService.GetGiftList();
            if (giftList == null || !giftList.Any())
            {
                result.Msg = "礼物配置信息无效";
                return result;
            }
            var gift = giftList.FirstOrDefault(m => m.id == giftId);
            if (gift == null)
            {
                result.Msg = "赠送的礼物无效";
                return result;
            }
            //扣除用户的金币
            var userAccount = this.CurrentRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (userAccount == null)
            {
                result.Msg = "用户信息无效";
                return result;
            }
            var roomInfo = this._roomService.Get(m => m.ID == roomId && m.OnlineStatus).FirstOrDefault();
            if (roomInfo == null)
            {
                result.Msg = "当前直播间无效";
                return result;
            }
            var liveAccount = this.CurrentRepository.Get(m => m.UserId == roomInfo.UserId).FirstOrDefault();
            if (liveAccount == null)
            {
                result.Msg = "主播信息无效";
                return result;
            }
            #endregion

            var now = DateTime.Now;

            #region 用户账户变动信息
            //用户账户变动
            var goldByUser = userAccount.Gold;
            var giving = gift.gold;
            userAccount.Gold -= giving;
            userAccount.UpdateTime = now;
            var result1 = this.CurrentRepository.Update(userAccount);
            if (result1 <= 0)
            {
                result.Msg = "用户送出礼物，扣除金币失败";
                return result;
            }
            //金币变动记录
            var result2 = this._goldRecordService.Add(new GoldRecord()
            {
                UserId = userId,
                ChangeType = (int)GoldChangeTypeEnum.Reduce,
                GoldBefore = goldByUser,
                GoldAfter = userAccount.Gold,
                Remark = $"用户送出礼物，减少金币{giving}",
                InsertTime = now,
                UpdateTime = now
            });
            if (result2.Code == ResultEnum.Error || result2.Data <= 0)
            {
                result.Msg = "用户送出礼物,增加金币变动记录失败";
                return result;
            }
            #endregion

            #region 主播账户变动信息
            //用户账户变动
            var goldByLive = liveAccount.Gold;
            var receival = gift.gold;
            liveAccount.Gold += receival;
            liveAccount.UpdateTime = now;
            var result3 = this.CurrentRepository.Update(liveAccount);
            if (result3 <= 0)
            {
                result.Msg = "主播获得礼物，增加金币失败";
                return result;
            }
            //金币变动记录
            var result4 = this._goldRecordService.Add(new GoldRecord()
            {
                UserId = roomInfo.UserId,
                ChangeType = (int)GoldChangeTypeEnum.Increase,
                GoldBefore = goldByLive,
                GoldAfter = liveAccount.Gold,
                Remark = $"主播获得礼物，增加金币{giving}",
                InsertTime = now,
                UpdateTime = now
            });
            if (result4.Code == ResultEnum.Error || result4.Data <= 0)
            {
                result.Msg = "主播获得礼物，增加金币变动记录失败";
                return result;
            }
            #endregion

            result.Data = userAccount.Gold;
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}