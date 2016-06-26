 
using CRM.Model;
namespace CRM.IDAL
{
	
    public partial interface IUserAccountRepository :IBaseRepository<UserAccount>
    {         
    }
	
    public partial interface IUserBaseInfoRepository :IBaseRepository<UserBaseInfo>
    {         
    }
	
    public partial interface IUserLoginInSideRepository :IBaseRepository<UserLoginInSide>
    {         
    }
	
    public partial interface IUserLoginOutSideRepository :IBaseRepository<UserLoginOutSide>
    {         
    }
	
}