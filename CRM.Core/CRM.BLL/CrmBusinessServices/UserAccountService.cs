﻿using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserAccountService : IUserAccountService
    {
        readonly IGiftService _giftService = new GiftService();
        readonly IIntegralRecordService _integralRecordService = new IntegralRecordService();
        readonly IGoldRecordService _goldRecordService = new GoldRecordService();
        readonly IRoomService _roomService = new RoomService();
        readonly IUserBaseService _userBaseService = new UserBaseService();
        /// <summary>
        /// 用户充值 增加金币
        /// </summary>
        /// <returns></returns>
        public Result<long> Recharge(int userId,decimal amount)
        {
            var result = new Result<long>();
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

            //获取用户信息
            var checkUser = this._userBaseService.CheckUser(userId);
            if (checkUser.Code == ResultEnum.Error)
            {
                result.Msg = checkUser.Msg;
                return result;
            }
            var userAccount = this.CurrentRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (userAccount == null)
            {
                result.Msg = "用户资金信息无效";
                return result;
            }
            #region 用户充值
            var now = DateTime.Now;
            //用户账户变动
            var gold = userAccount.Gold;
            var rechare = (int)Math.Ceiling(amount) * 10;
            userAccount.Gold += rechare;
            userAccount.UpdateTime = now;
            var iRet1 = this.CurrentRepository.Update(userAccount);
            if (iRet1 <= 0)
            {
                result.Msg = "用户充值失败";
                return result;
            }
            //资金变动记录
            //var iRet2 = this._moneyRecordService.Add(new MoneyRecord()
            //{
            //    UserId = userId,
            //    ChangeType = (int)MoneyChangeTypeEnum.Recharge,
            //    Amount = amount,
            //    Remark = "用户充值，兑换成金币",
            //    InsertTime = now,
            //    UpdateTime = now
            //});
            //金币变动记录
            var iRet3 = this._goldRecordService.Add(new GoldRecord()
            {
                UserId = userId,
                ChangeType = (int)GoldChangeTypeEnum.Increase,
                GoldBefore = gold,
                GoldAfter = userAccount.Gold,
                Remark = $"用户充值，增加金币{rechare}",
                InsertTime = now,
                UpdateTime = now
            });
            if (iRet3.Code == ResultEnum.Error || iRet3.Data.UserId <= 0)
            {
                result.Msg = "注册失败3";
                return result;
            }
            #endregion
            result.Data = userAccount.Gold;
            result.Code= ResultEnum.Success;
            return result;
        }
        /// <summary>
        /// 用户购买礼物 赠送主播
        /// </summary>
        /// <returns></returns>
        public Result<long> Cost(int userId,int roomId,int giftId)
        {
            var result = new Result<long>();
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
            if (userAccount.Gold <= 0)
            {
                result.Msg = "金币数量不足";
                return result;
            }
            var roomInfo = this._roomService.Get(m => m.ID == roomId && m.Status !=(int)UserBaseStausEnum.Normal).FirstOrDefault();
            if (roomInfo == null)
            {
                result.Msg = "当前直播间无效";
                return result;
            }
            if (roomInfo.UserId == userId)
            {
                result.Msg = "不可以给自己送礼物";
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
            var giving = gift.gold;

            #region 用户账户变动信息
            //用户账户变动
            var goldByUser = userAccount.Gold;
            userAccount.Gold -= giving;
            userAccount.Contribution += giving;
            userAccount.UpdateTime = now;
            var result1 = this.CurrentRepository.Update(userAccount);
            if (result1 <= 0)
            {
                result.Msg = $"用户送出礼物给主播[{liveAccount.UserId}]，扣除金币失败";
                return result;
            }
            //金币变动记录
            var result2 = this._goldRecordService.Add(new GoldRecord()
            {
                UserId = userId,
                ChangeType = (int)GoldChangeTypeEnum.Reduce,
                GoldBefore = goldByUser,
                GoldAfter = userAccount.Gold,
                Remark = $"用户送出礼物给主播[{liveAccount.UserId}]，减少金币[{giving}]",
                InsertTime = now,
                UpdateTime = now
            });
            if (result2.Code == ResultEnum.Error || result2.Data.ID <= 0)
            {
                result.Msg = $"用户送出礼物给主播[{liveAccount.UserId}],增加金币变动记录失败";
                return result;
            }
            #endregion

            #region 主播账户变动信息
            //用户账户变动
            var integral = liveAccount.Integral;
            liveAccount.Integral += giving;
            liveAccount.UpdateTime = now;
            var result3 = this.CurrentRepository.Update(liveAccount);
            if (result3 <= 0)
            {
                result.Msg = $"主播获得粉丝[{userId}]的礼物，增加积分失败";
                return result;
            }
            //积分变动记录
            var result4 = this._integralRecordService.Add(new IntegralRecord()
            {
                UserId = roomInfo.UserId,
                ChangeType = (int)IntegralChangeTypeEnum.IncreaseByGetGift,
                IntegralBefore = integral,
                IntegralAfter = liveAccount.Integral,
                Remark = $"主播获得粉丝[{userId}]的礼物，增加积分{giving}",
                InsertTime = now,
                UpdateTime = now
            });
            if (result4.Code == ResultEnum.Error || result4.Data.ID <= 0)
            {
                result.Msg = $"主播获得粉丝[{userId}]的礼物，增加积分变动记录失败";
                return result;
            }
            #endregion

            result.Data = userAccount.Gold;
            result.Code = ResultEnum.Success;
            return result;
        }
        /// <summary>
        /// 用户积分兑现 用户减少积分 增加收益
        /// </summary>
        /// <returns></returns>
        public Result<decimal> Profit(int userId, int gold)
        {
            var result = new Result<decimal>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            if (gold <= 0)
            {
                result.Msg = "金币兑换无效";
                return result;
            }
            #endregion
            //获取用户信息
            var checkUser = this._userBaseService.CheckUser(userId);
            if (checkUser.Code == ResultEnum.Error)
            {
                result.Msg = checkUser.Msg;
                return result;
            }
            var userAccount = this.CurrentRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (userAccount == null)
            {
                result.Msg = "用户资金信息无效";
                return result;
            }
            var now = DateTime.Now;
            var integral = userAccount.Integral;
            userAccount.Integral -= gold;
            userAccount.Profit += gold;
            userAccount.UpdateTime = now;
            //金币变动记录
            var goldRecord = this._integralRecordService.Add(new IntegralRecord()
            {
                UserId = userId,
                ChangeType = (int)IntegralChangeTypeEnum.Reduce,
                IntegralBefore = integral,
                IntegralAfter = userAccount.Integral,
                Remark = $"用户积分兑换，减少积分[{integral}]",
                InsertTime = now,
                UpdateTime = now
            });
            if (goldRecord.Code == ResultEnum.Error || goldRecord.Data.ID <= 0)
            {
                result.Msg = "用户积分兑换,增加积分变动记录失败";
                return result;
            }
            result.Data = userAccount.Profit;
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}