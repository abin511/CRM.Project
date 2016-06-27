 

using CRM.Model;
namespace CRM.BLL
{
	
	public partial class UserAccountService:BaseCrmManageService<UserAccount>,IBLL.IUserAccountService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserAccountRepository;
        }  
    }
	
	public partial class UserBaseInfoService:BaseCrmManageService<UserBaseInfo>,IBLL.IUserBaseInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserBaseInfoRepository;
        }  
    }
	
	public partial class UserLoginInSideService:BaseCrmManageService<UserLoginInSide>,IBLL.IUserLoginInSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserLoginInSideRepository;
        }  
    }
	
	public partial class UserLoginOutSideService:BaseCrmManageService<UserLoginOutSide>,IBLL.IUserLoginOutSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserLoginOutSideRepository;
        }  
    }
	
}