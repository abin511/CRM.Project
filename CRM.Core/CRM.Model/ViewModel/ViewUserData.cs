namespace CRM.Model
{
    public class ViewUserData
    {
        /// <summary>
        /// token  加密(ID + 当前时间)
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 直播的url地址，直播时，使用此地址，每次登陆，都会改变
        /// </summary>
        public string liveurl { get; set; }
        /// <summary>
        /// 播放的url地址，用于在直播时，分享时的播放地址，每次登陆，都会改变
        /// </summary>
        public string playurl { get; set; }
    }
}
