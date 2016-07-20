using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class GiftService:IGiftService
    {
        readonly ICustomConfigService _customConfigService = new CustomConfigService();
        private const string ConfigKey = "gift";
        /// <summary>
        /// 主播推流
        /// </summary>
        /// <returns></returns>
        public List<ViewGift> GetGiftList()
        {
            var config = _customConfigService.Get(m => m.KeyName.ToLower() == ConfigKey).FirstOrDefault();
            return config.GetKey<List<ViewGift>>();
        }
    }
}