 
using CRM.IDAL;
namespace CRM.DAL
{
    public partial class CrmManageDbSession:ICrmManageDbSession 
    {   
		/// <summary>
        /// 代表当前应用程序跟数据库的回话内所有的实体变化，更新会数据库
		/// UintWork单元工作模式
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()  
        {   
            //调用EF上下文的SaveChanges的方法
            return EFContextFactory.GetCrmManageDbContext().SaveChanges();
        }
		
		public IActionGroupRepository ActionGroupRepository { get { return new ActionGroupRepository(); } }
		
		public IActionInfoRepository ActionInfoRepository { get { return new ActionInfoRepository(); } }
		
		public IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get { return new R_UserInfo_ActionInfoRepository(); } }
		
		public IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get { return new R_UserInfo_RoleRepository(); } }
		
		public IRoleRepository RoleRepository { get { return new RoleRepository(); } }
		
		public IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }
		}
}