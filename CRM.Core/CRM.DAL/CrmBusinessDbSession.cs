 
using CRM.IDAL;
namespace CRM.DAL
{
	//T4模板自动生成，请勿编辑代码
    public partial class CrmBusinessDbSession:ICrmBusinessDbSession
    {   
		
		public IUserAccountRepository UserAccountRepository { get { return new UserAccountRepository(); } }
		
		public IUserBaseInfoRepository UserBaseInfoRepository { get { return new UserBaseInfoRepository(); } }
		
		public IUserLoginInSideRepository UserLoginInSideRepository { get { return new UserLoginInSideRepository(); } }
		
		public IUserLoginOutSideRepository UserLoginOutSideRepository { get { return new UserLoginOutSideRepository(); } }
		}
}