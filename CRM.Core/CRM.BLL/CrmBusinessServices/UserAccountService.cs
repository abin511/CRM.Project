using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserAccountService : IUserAccountService
    {
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

            var model = DbSession.UserAccountRepository.Get(m => m.UserId == userId).FirstOrDefault();
            if (model == null)
            {
                result.Msg = "用户资金信息无效";
                return result;
            }
            
            var now = DateTime.Now;
            var gold = model.Gold;
            //用户账户变动
            var rechare = (int)Math.Ceiling(amount) * 10;
            model.Gold += rechare;
            model.Contribution += rechare;
            model.Profit += rechare;
            model.UpdateTime = now;
            var result1 = DbSession.UserAccountRepository.Update(model);
            if (result1 <= 0)
            {
                result.Msg = "用户充值失败";
                return result;
            }
            //资金变动记录
            var result2 = DbSession.MoneyRecordRepository.Add(new MoneyRecord()
            {
                UserId = userId,
                ChangeType = (int)MoneyChangeTypeEnum.Recharge
            });
            //金币变动记录
            var result3 = DbSession.GoldRecordRepository.Add(new GoldRecord()
            {
                UserId = userId,
                ChangeType = (int)GoldChangeTypeEnum.Increase,
                GoldBefore = gold,
                GoldAfter = model.Gold,
                Remark = $"用户充值，增加金币{rechare}",
                InsertTime = now,
                UpdateTime = now
            });
            result.Data = model.Gold;
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

            return result;
        }
    }
}