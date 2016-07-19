using System.Linq;
using System.Net.Http;
using CRM.BLL;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Trading
{
    /// <summary>
    ///礼物清单
    /// </summary>
    public class GiftListController : BaseApiController
    {
        readonly ICustomConfigService _customConfigService = new CustomConfigService();
        private const string ConfigKey = "gift";
        /// <summary>
        /// 获取配置的礼物清单
        /// </summary>
        public HttpResponseMessage Get()
        {
            return base.Wrapper<ViewGiftList>(() =>
            {
                var config = _customConfigService.Get(m=>m.KeyName.ToLower() == ConfigKey).FirstOrDefault();
                return new Result<ViewGiftList>()
                {
                    Code = ResultEnum.Success,
                    Data = config.GetKey<ViewGiftList>()
                };
            });
        }
    }
}
