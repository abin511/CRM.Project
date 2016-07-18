using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IRoomService
    {
        /// <summary>
        /// 主播推流
        /// </summary>
        /// <returns></returns>
        Result<int> Live(int userId, string title, string cover);
    }
}
