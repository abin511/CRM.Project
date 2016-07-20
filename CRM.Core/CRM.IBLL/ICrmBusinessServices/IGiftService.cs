using System.Collections.Generic;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IGiftService
    {
        /// <summary>
        /// 获取礼物信息
        /// </summary>
        /// <returns></returns>
        List<ViewGift> GetGiftList();
    }
}
