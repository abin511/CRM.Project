 
using CRM.Model;

namespace CRM.IBLL
{
	
	public partial interface IUserAccountService:IBaseService<UserAccount>
    {   
    }
	
	public partial interface IUserBaseInfoService:IBaseService<UserBaseInfo>
    {   
    }
	
	public partial interface IUserLoginInSideService:IBaseService<UserLoginInSide>
    {   
    }
	
	public partial interface IUserLoginOutSideService:IBaseService<UserLoginOutSide>
    {   
    }
	
}