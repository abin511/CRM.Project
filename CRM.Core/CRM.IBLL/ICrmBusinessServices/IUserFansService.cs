using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserFansService
    {
        /// <summary>
        /// 关注或取消关注某人
        /// </summary>
        /// <returns></returns>
        Result<int> Attention(int userId, string userNumber);
        /// <summary>
        /// 关注或取消关注某人
        /// </summary>
        /// <returns></returns>
        Result<bool> IsAttention(int userId, string userNumber);
    }
}
