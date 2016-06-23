using CRM.IDAL;

namespace CRM.DAL
{
    /// <summary>
    /// 简单工厂实现低耦合，数据库访问层的统一入口
    /// </summary>
    /// 尽量的去依赖接口编程
    public partial class RepositoryFactory
    {
       
        public static IUserInfoRepository UserInfoRepository
        {
            get
            {
                return new UserInfoRepository();
            }
        }

        public static IRoleRepository RoleRepository
        {
            get
            {
                return new RoleRepository();
            }
        }

    }
}
