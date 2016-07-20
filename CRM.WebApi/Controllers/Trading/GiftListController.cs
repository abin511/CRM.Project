using System.Collections.Generic;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Trading
{
    /// <summary>
    ///礼物清单
    /// </summary>
    public class GiftListController : BaseApiController
    {
        readonly IGiftService _giftService = new GiftService();
        /// <summary>
        /// 获取配置的礼物清单
        /// </summary>
        public HttpResponseMessage Get()
        {
            return base.Wrapper(() => new Result<List<ViewGift>>()
            {
                Code = ResultEnum.Success,
                Data = this._giftService.GetGiftList()
            });
        }
    }
}
