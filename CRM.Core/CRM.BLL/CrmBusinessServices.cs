 
using CRM.Model;
using CRM.IBLL;
namespace CRM.BLL
{
	//T4模板自动生成，请勿编辑代码
	
	public partial class UserAccountService:BaseCrmBusinessServices<UserAccount>,IUserAccountService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserAccountRepository;
        }  
    }
	
	public partial class UserBaseInfoService:BaseCrmBusinessServices<UserBaseInfo>,IUserBaseInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserBaseInfoRepository;
        }  
    }
	
	public partial class UserLoginInSideService:BaseCrmBusinessServices<UserLoginInSide>,IUserLoginInSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserLoginInSideRepository;
        }  
    }
	
	public partial class UserLoginOutSideService:BaseCrmBusinessServices<UserLoginOutSide>,IUserLoginOutSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserLoginOutSideRepository;
        }  
    }
}