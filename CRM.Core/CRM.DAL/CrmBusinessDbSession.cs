 
using CRM.IDAL;
namespace CRM.DAL
{
    public partial class DbSession:IDbSession
    {   
		
		public IUserAccountRepository UserAccountRepository { get { return new UserAccountRepository(); } }
		
		public IUserBaseInfoRepository UserBaseInfoRepository { get { return new UserBaseInfoRepository(); } }
		
		public IUserLoginInSideRepository UserLoginInSideRepository { get { return new UserLoginInSideRepository(); } }
		
		public IUserLoginOutSideRepository UserLoginOutSideRepository { get { return new UserLoginOutSideRepository(); } }
		}
}