 
using CRM.IDAL;
namespace CRM.DAL
{
    public partial class CrmBusinessDbSession:ICrmBusinessDbSession 
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
		
		public IUserAccountRepository UserAccountRepository { get { return new UserAccountRepository(); } }
		
		public IUserBaseInfoRepository UserBaseInfoRepository { get { return new UserBaseInfoRepository(); } }
		
		public IUserLoginInSideRepository UserLoginInSideRepository { get { return new UserLoginInSideRepository(); } }
		
		public IUserLoginOutSideRepository UserLoginOutSideRepository { get { return new UserLoginOutSideRepository(); } }
		}
}